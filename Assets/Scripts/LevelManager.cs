using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using static UnityEditor.PlayerSettings;
#endif


public class LevelManager : MonoBehaviour
{
    [Tooltip("フルーツ削除時のサウンド")]
    public AudioClip removeSound7;
    public AudioClip removeSound8;
    public AudioClip removeSound9;
    public AudioClip changeSounds;
    private AudioSource _audioSource;

    private List<Fruit> _AllFruits = new List<Fruit>(); // 全フルーツのリスト
    public List<GameObject> gameObjects = new List<GameObject>(); // 最大16種類のフルーツ
    public List<Fruit> _SelectFruits = new List<Fruit>(); // プレイヤーが選択中のフルーツ
    private string _SelectID = ""; // 選択されたフルーツの種類（ID）
    private int _Score = -1; // スコア
    public int CurrentScore => _Score;

    private float _CurrentTime = 256; // 残り時間
    private bool _IsPlaying = true; // ゲームが進行中かどうか
    public bool AllowFruitSwap = true; // Inspector で交換可否を設定
    public int minSpawnLevel = 0; // 最小レベル（Inspector で変更可能）
    public int maxSpawnLevel = 5; // 最大レベル（Inspector で変更可能）

    public static LevelManager Instance { get; private set; }

    public LineRenderer LineRenderer;
    public GameObject BomPrefab;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimerText;
    public GameObject finishDialog;

    public int FruitDestroyCount = 3;
    public float FruitConnectRange = 0.5f;
    public int BomSpawnCount = 5;
    public float BomDestroyRange = 1.5f;
    public float PlayTime = 60;
    private bool isSpawningFruits = false;

    private int removedFruitsCount;

    private IEnumerator Start()
    {
        _audioSource = GetComponent<AudioSource>();

        while (!SaveManager.Instance.IsLoaded)
        {
            yield return null; // 次のフレームまで待つ
        }

        float loadedTime = SaveManager.Instance.GetSavedTime();
        int loadedScore = SaveManager.Instance.GetSavedScore();

        _CurrentTime = loadedTime > 0 ? loadedTime : 9999;
        _Score = loadedScore >= 0 ? loadedScore : 0;
        ScoreText.text = loadedScore.ToString();

        StartCoroutine(FruitSpawnCoroutine(40));
        _IsPlaying = true;

        InvokeRepeating("UpdateTimer", 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsPlaying)
        {
            LineRendererUpdate();
        }
    }

    private void UpdateTimer()
    {
        if (_IsPlaying)
        {
            _CurrentTime -= 0.5f;
            if (_CurrentTime <= 0)
            {
                _CurrentTime = 0;
                _IsPlaying = false;
                finishDialog.SetActive(true);
            }
            TimerText.text = ((int)_CurrentTime).ToString();
        }
    }

    private void LineRendererUpdate()
    {
        if (_SelectFruits.Count >= 2)
        {
            LineRenderer.positionCount = _SelectFruits.Count;
            LineRenderer.SetPositions(_SelectFruits.Select(fruit => fruit.transform.position).ToArray());
            LineRenderer.gameObject.SetActive(true);
        }
        else LineRenderer.gameObject.SetActive(false);
    }

    private IEnumerator FruitSpawnCoroutine(int count)
    {
        if (isSpawningFruits) yield break;
        isSpawningFruits = true;

        if (gameObjects == null || gameObjects.Count == 0)
        {
            //Debug.LogError("フルーツのプレハブリストが空です！");
            isSpawningFruits = false;
            yield break;
        }

        int StartX = -2;
        int StartY = 5;
        int X = 0, Y = 0;
        int MaxX = 5;

        minSpawnLevel = Mathf.Clamp(minSpawnLevel, 0, Mathf.Min(gameObjects.Count - 1, 15));
        maxSpawnLevel = Mathf.Clamp(maxSpawnLevel, minSpawnLevel, Mathf.Min(gameObjects.Count - 1, 15));

        // 余計なフルーツが出る可能性を防ぐため、新しいリストを作成
        List<Fruit> spawnedFruits = new List<Fruit>();

        for (int i = 0; i < count; i++)
        {
            var Position = new Vector3(StartX + X, StartY + Y, 0);
            var prefab = gameObjects[Random.Range(minSpawnLevel, maxSpawnLevel + 1)];
            var FruitObject = Instantiate(prefab, Position, Quaternion.identity);
            Fruit newFruit = FruitObject.GetComponent<Fruit>();

            spawnedFruits.Add(newFruit);
            _AllFruits.Add(newFruit);

            X++;
            if (X == MaxX)
            {
                X = 0;
                Y++;
            }

            if (i % 5 == 0) yield return null; // 5個ごとにフレームをまたぐ
        }

        isSpawningFruits = false;
    }

    public void PlaySound(AudioClip clip)
    {
        if (_audioSource != null && clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }

    public void CleanupFruits()
    {
        //_AllFruits.RemoveAll(fruit => fruit == null);
        //_SelectFruits.Clear();
    }

    private void FruitSpawn(int count)
    {
        if (gameObjects == null || gameObjects.Count == 0)
        {
            //Debug.LogError("フルーツのプレハブリストが空です！");
            return;
        }

        var StartX = -2;
        var StartY = 5;
        var X = 0;
        var Y = 0;
        var MaxX = 5;

        // minSpawnLevel と maxSpawnLevel を制限（0～15の範囲に収める）
        minSpawnLevel = Mathf.Clamp(minSpawnLevel, 0, Mathf.Min(gameObjects.Count - 1, 15));
        maxSpawnLevel = Mathf.Clamp(maxSpawnLevel, minSpawnLevel, Mathf.Min(gameObjects.Count - 1, 15));

        for (int i = 0; i < count; i++)
        {
            var Position = new Vector3(StartX + X, StartY + Y, 0);
            //var fruit = Instantiate(fruitPrefab, pos, Quaternion.identity);
            //var prefab = gameObjects[Random.Range(0, 2)];
            var prefab = gameObjects[Random.Range(minSpawnLevel, maxSpawnLevel + 1)]; // 指定範囲内のフルーツを選択
            var FruitObject = Instantiate(prefab, Position, Quaternion.identity);
            _AllFruits.Add(FruitObject.GetComponent<Fruit>());

            X++;
            if (X == MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }
    public void FruitDown(Fruit fruit)
    {
        //if (!_IsPlaying) return;
        //Debug.Log($"FruitDown呼び出された: {fruit.name}");
        if (!_SelectFruits.Contains(fruit))
        {
            _SelectFruits.Add(fruit); // 選択中のフルーツリストに追加
            fruit.SetIsSelect(true);  // 選択状態にする
            _SelectID = fruit.ID;  // 選択中のフルーツのIDを記録
            //Debug.Log($"追加されたフルーツ: {fruit.name}（合計 {_SelectFruits.Count}）");
        }
        else
        {
            //Debug.Log($"{fruit.name} はすでに選択済みだよ！");
        }
    }


    public void FruitEnter(Fruit fruit)
    {
        if (!_IsPlaying) return;

        var lastFruit = _SelectFruits.Count > 0 ? _SelectFruits[_SelectFruits.Count - 1] : null;
        if (lastFruit == null) return;

        // **型を string に統一**
        string selectedFruitID = _SelectFruits.Count > 0 ? _SelectFruits[0].ID : "";

        if (_SelectFruits.Count > 0)
        {
            var lastFruitPos = _SelectFruits[_SelectFruits.Count - 1].transform.position;
            var currentFruitPos = fruit.transform.position;
            var Length = Vector2.Distance(new Vector2(lastFruitPos.x, lastFruitPos.y), new Vector2(currentFruitPos.x, currentFruitPos.y));

            if (Length < FruitConnectRange)
            {
                if (fruit.IsSelect)
                {
                    if (_SelectFruits.Count >= 2 && _SelectFruits[_SelectFruits.Count - 2] == fruit)
                    {
                        var RemoveFruit = _SelectFruits[_SelectFruits.Count - 1];
                        RemoveFruit.SetIsSelect(false);
                        _SelectFruits.Remove(RemoveFruit);
                    }
                }
                else
                {
                    // **異なるフルーツを選択不可にする**
                    if (_SelectFruits.Count >= 2 && fruit.ID != selectedFruitID)
                    {
                        //Debug.Log("異なるフルーツにはアクセスできません: " + fruit.name);
                        return;
                    }

                    // **同じ種類なら通常の選択処理**
                    if (_SelectID == fruit.ID)
                    {
                        _SelectFruits.Add(fruit);
                        fruit.SetIsSelect(true);
                    }
                    // **1個目 or 2個目未満なら交換可能**
                    else if (_SelectFruits.Count < 2)
                    {
                        SwapFruits(lastFruit, fruit);
                    }
                }
            }
        }
    }

    // 2つのフルーツの位置を交換
    private void SwapFruits(Fruit fruit1, Fruit fruit2)
    {
        AudioManager.Instance.PlaySound(changeSounds);
        Vector3 tempPos = fruit1.transform.position;
        fruit1.transform.position = fruit2.transform.position;
        fruit2.transform.position = tempPos;
    }

    public void FruitUp()
    {
        if (!_IsPlaying) return;
        //Debug.Log($"_SelectFruits.Count: {_SelectFruits.Count}, FruitDestroyCount: {FruitDestroyCount}");

        if (_SelectFruits.Count >= FruitDestroyCount)
        {
            Vector3 spawnPosition = _SelectFruits[_SelectFruits.Count - 1].transform.position;
            Fruit firstFruit = _SelectFruits[0];
            
            int fruitLevel = firstFruit.Level;
            removedFruitsCount = _SelectFruits.Count;
            //Debug.Log("削除するフルーツの数: " + removedFruitsCount);

            StartCoroutine(DestroyFruitsCoroutine(_SelectFruits));

            if (removedFruitsCount >= BomSpawnCount)
            {
                foreach (var prefab in gameObjects)
                {
                    Fruit fruitComponent = prefab.GetComponent<Fruit>();
                    if (fruitComponent != null && fruitComponent.Level == fruitLevel + 1)
                    {
                        var newFruit = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        newFruit.GetComponent<Fruit>().Level = fruitComponent.Level;
                        removedFruitsCount--;
                        break;
                    }
                }
            }
            else
            {
                //Debug.Log("制約により強化フルーツは生成されませんでした。");
            }

            //removedFruitsCount = Mathf.Max(0, removedFruitsCount);
            
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var fruitComponent = gameObjects[i].GetComponent<Fruit>();
                
            }
            //Debug.Log("追加するフルーツの数: " + removedFruitsCount);
            // ランダムな位置に新しいフルーツをスポーン
            FruitSpawn(removedFruitsCount);


            //for (int i = 0; i < removedFruitsCount; i++)
            //{
            //    Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(3f, 5f), 0);

            //    int maxIndex = Mathf.Min(maxSpawnLevel, gameObjects.Count - 1);     // ADD
            //    int randomIndex = Random.Range(minSpawnLevel, maxIndex);            // ADD

            //    var randomFruit = Instantiate(gameObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            //    var fruitComponent = gameObjects[randomIndex].GetComponent<Fruit>();

            //    if (fruitComponent != null)
            //    {
            //        randomFruit.GetComponent<Fruit>().Level = fruitComponent.Level;
            //    }
            //    else
            //    {
            //        Debug.LogError("生成されたフルーツに Fruit コンポーネントがありません！");
            //    }
            // }
        }
        else
        {
            //Debug.Log("削除するフルーツの数が足りません。");
            foreach (var fruit in _SelectFruits)
                fruit.SetIsSelect(false);
        }

        // 選択状態をリセット
        _SelectID = "";
        _SelectFruits.Clear();
    }
    public void BomDown(Bom bom)
    {
        var RemoveFruits = new List<Fruit>(); // 削除対象のフルーツリストを作成

        foreach (var FruitItem in _AllFruits) // 全フルーツをチェック
        {
            var Length = (FruitItem.transform.position - bom.transform.position).magnitude; // 爆弾との距離を計算
            if (Length < BomDestroyRange) // 一定範囲内なら
                RemoveFruits.Add(FruitItem); // 削除リストに追加
        }

        DestroyFruits(RemoveFruits); // 削除リストのフルーツを消す
        Destroy(bom.gameObject); // 爆弾オブジェクト自体も消す
    }

    private void DestroyFruits(List<Fruit> fruits)
    {
        if (AudioManager.Instance != null && removeSound7 != null && removeSound8 != null && removeSound9 != null)
        {
            AudioClip[] sounds = { removeSound7, removeSound8, removeSound9 };
            int soundIndex = UnityEngine.Random.Range(0, sounds.Length); // 0, 1, 2 のいずれかを取得

            AudioManager.Instance.PlaySound(sounds[soundIndex]); // AudioClip を渡す
        }

        foreach (var fruit in fruits)
        {
            Destroy(fruit.gameObject);
            _AllFruits.Remove(fruit);
        }
        AddScore(fruits.Count);
    }
    private void AddScore(int fruitCount)
    {
        _Score += (int)(fruitCount * 100 * (1 + (fruitCount - 3) * 0.1f));
        ScoreText.text = _Score.ToString();
    }
    private IEnumerator DestroyFruitsCoroutine(List<Fruit> fruits)
    {
        if (AudioManager.Instance != null && removeSound7 != null && removeSound8 != null && removeSound9 != null)
        {
            AudioClip[] sounds = { removeSound7, removeSound8, removeSound9 };
            int soundIndex = UnityEngine.Random.Range(0, sounds.Length); // 0, 1, 2 のいずれかを取得

            AudioManager.Instance.PlaySound(sounds[soundIndex]); // AudioClip を渡す
        }
        int removedCount = fruits.Count;
        foreach (var fruit in fruits)
        {
            Destroy(fruit.gameObject);
            _AllFruits.Remove(fruit);
        }
        AddScore(fruits.Count);
        yield return new WaitForEndOfFrame(); // 削除が適用されるまで待つ

        removedFruitsCount = removedCount; // 正しくカウントを更新
    }

}
