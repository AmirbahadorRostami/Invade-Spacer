using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace SpaceInvader
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameObject _PlayerPrefab;
        [SerializeField] private GameObject _EnemyPrefab;
        [SerializeField] private UIManager _UIManager;
        [SerializeField] private GameObject Enviroment;
        
        [SerializeField] private List<GameObject> Enemies;
        [SerializeField] private int Score = 0;
        
        public int numberOfEnemies ;
        public PlayerShip player { get; private set; }
        public static GameManager _Instance { get; private set; }
        
        private void Awake()
        {
            if (_Instance != null && _Instance != this)
            {
                Destroy(this);
                return;
            }
            _Instance = this;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _UIManager.ChangeUI(UIView.MainMenu);
        }
        
        public void StartGame()
        {
            _UIManager.ChangeUI(UIView.GameView);
            Enviroment.SetActive(true);
            player = Instantiate(_PlayerPrefab, transform.position, Quaternion.identity).GetComponent<PlayerShip>();
            _UIManager.setHealth(player.Health);
            player.PlayerTakeDamage += OnPlayerTakeDamageEvent;
            player.ShipDiedEvent += OnPlayerDiedEvent;
            
            // instantiate a bunch of enemies 
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 randPos = new Vector3(Random.Range(-1, 1) * 100 + 50, 0f, Random.Range(-1, 1) * 100 + 50);
                
                GameObject go = Instantiate(_EnemyPrefab, randPos, Quaternion.identity);
                Enemies.Add(go);
            }
        }


        public void Restart()
        {
            Score = 0;
            foreach (var Enemy in Enemies)
            {
                Destroy(Enemy);
            }
            
            StartGame();
        }


        public void Quit ()
        {
            Application.Quit();
        }

        private void OnDisable()
        {
            player.PlayerTakeDamage -= OnPlayerTakeDamageEvent;
            player.ShipDiedEvent -= OnPlayerDiedEvent;
        }

        void OnPlayerDiedEvent(ShipType type)
        {
            // Game over
            Debug.Log("Player is dead");
            // bring up game over menu and restart
            _UIManager.ChangeUI(UIView.PauseMenu);
        }
        
        void OnPlayerTakeDamageEvent()
        {
            Debug.Log(player.Health);
            _UIManager.setHealth(player.Health);
        }
    }

}
