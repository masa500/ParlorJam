using NewgroundsUnityAPIHelper.Helper.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gameflow : MonoBehaviour
{
    private GameplayMode _gameplayMode;

    [Header("Scripts")]
    [SerializeField] private TutorialLogic tutorialLogic;
    [SerializeField] private GhostSpawner ghostSpawner;
    [SerializeField] private InputReader controls;
    [Header("GameObjects")]
    [SerializeField] private RectTransform transitionFadeObject;
    [SerializeField] private SpriteRenderer blackOutMask;
    [SerializeField] private SpriteMask lineMask;
    [SerializeField] private GameObject linesContainer;
    [SerializeField] private GameObject seekCursor;
    [SerializeField] private TextMeshProUGUI roundCounter;
    [SerializeField] private TextMeshProUGUI timerText;
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject tryAgainUI;
    [SerializeField] private GameObject victoryUI;
    [Header("Buttons")]
    [SerializeField] private Button playEasyButton;
    [SerializeField] private Button playHardButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button winButton;
    [SerializeField] private Button tryButton;
    [Header("Timers")]
    [SerializeField] private float transitionFadeInDuration = 1f;
    [SerializeField] private float transitionFadeOutDuration = 1f;
    [SerializeField] private float waitHideDuration = 3f;
    [SerializeField] private float hideDuration = 1f;
    [SerializeField] private float blackOutDuration = 1f;
    [SerializeField] private float gameplayDuration = 6f;
    [SerializeField] private float restDuration = 1f;
    [SerializeField] private float maskRevealDuration = 1f;
    [Header("Sound")]
    [SerializeField] private GameObject soundPrefab;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip initSound;
    [SerializeField] private AudioClip clockSound;
    private GameConfiguration _gameConfig;
    private Round _round;

    void Awake()
    {

        _gameplayMode = new GameplayMode();
        _gameConfig = new GameConfiguration();
        _round = new Round();
        _round.current = 1;

        //Menu State
        _gameConfig.AddInitialState(GameConfiguration.MainMenuState,
            new MenuState(mainMenuUI, tutorialUI, transitionFadeObject, transitionFadeOutDuration, playEasyButton, creditsButton, tutorialButton, playHardButton, _gameplayMode));

        //Tutorial State
        _gameConfig.AddState(GameConfiguration.TutorialState,
            new TutorialState(tutorialLogic, transitionFadeObject, transitionFadeInDuration, tutorialButton));

        //Spawner State
        _gameConfig.AddState(GameConfiguration.SpawnerState,
            new SpawnerState(transitionFadeOutDuration, transitionFadeObject, ghostSpawner, _round, tutorialUI, gameplayUI, lineMask, linesContainer, _gameplayMode, roundCounter));

        //Hide State
        _gameConfig.AddState(GameConfiguration.HideState,
            new HideState(waitHideDuration, hideDuration, blackOutDuration, blackOutMask, ghostSpawner.gameObject, controls, timerText, gameplayDuration));

        //Gameplay
        _gameConfig.AddState(GameConfiguration.GameplayState,
            new GameplayState(gameplayDuration, ghostSpawner.gameObject, _round, seekCursor, controls, soundPrefab, initSound, roundCounter, timerText, clockSound, _gameplayMode));

        //Win State
        _gameConfig.AddState(GameConfiguration.WinState,
            new WinState(ghostSpawner.gameObject, restDuration, maskRevealDuration, blackOutMask, transitionFadeObject, transitionFadeInDuration, soundPrefab, winSound));

        //Lose State
        _gameConfig.AddState(GameConfiguration.LoseState,
            new LoseState(ghostSpawner.gameObject, restDuration, maskRevealDuration, blackOutMask, soundPrefab, loseSound));

        //Try Again State
        _gameConfig.AddState(GameConfiguration.TryAgainState,
            new TryAgainState(transitionFadeObject, transitionFadeInDuration, tryButton, tryAgainUI, _round, roundCounter));

        //Victory State
        _gameConfig.AddState(GameConfiguration.VictoryState,
            new VictoryState(victoryUI, transitionFadeObject, playEasyButton, playHardButton, creditsButton, winButton, mainMenuUI, transitionFadeOutDuration, _round, gameplayUI, _gameplayMode));
    }

    void Start()
    {
        mainMenuUI.SetActive(true);
        tutorialUI.SetActive(false);
        gameplayUI.SetActive(false);
        tryAgainUI.SetActive(false);
        victoryUI.SetActive(false);

        transitionFadeObject.gameObject.SetActive(true);
        transitionFadeObject.sizeDelta = new Vector2(1200f, 800f);

        NewgroundsAPIHelper.Instance.LoadMedals();

        StartState(_gameConfig.GetInitialState());
    }

    private async void StartState(IState state, object data = null)
    {
        while (true)
        {
            var resultData = await state.DoAction(data);
            var nextState = _gameConfig.GetState(resultData.NextStateId);
            state = nextState;
            data = resultData.ResultData;
        }
    }
}
