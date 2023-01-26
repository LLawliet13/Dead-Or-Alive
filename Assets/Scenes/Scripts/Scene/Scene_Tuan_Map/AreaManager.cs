using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    //the model of world
    private List<ArenaBlock> arenaBlocks;
    public GameObject arenaBlockPrefab;
    public GameObject playerReference;
    [SerializeField]
    public float _arenaBlockWidth = 33.1f;
    [SerializeField]
    public float _arenaBlockHeight = 33f;
    public int _playerArenaBlockPosition;
    public int PlayerArenaBlockPosition
    {
        get { return _playerArenaBlockPosition; }
        set {
                if(_playerArenaBlockPosition != value)
                {
                    _playerArenaBlockPosition = value;
                    UpdateArenaModel();
                }
        }
    }
/*
 * awake is called when the script instance is being loaded
 */
    private void Awake()
    {
        arenaBlocks = new List<ArenaBlock>(new ArenaBlock[9]);
        InitArena();
        playerReference = GameObject.FindGameObjectWithTag("Player");
        PlayerArenaBlockPosition = arenaBlocks[4].Position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerReference == null)
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
        }
        PlayerArenaBlockPosition = GetArenaPositionClosestToPlayer();
    }
    private void InitArena()
    {
        for(int i = 0; i < 9; i++)
        {
            //instantiate (kh?i t?o) the gameObject/arenaBlock
            GameObject go = Instantiate(arenaBlockPrefab);
            //create arenaBlock
            ArenaBlock ab = new ArenaBlock()
            {
                Position = i,
                TheObject = go
            };
            //add it to the list arenaBlocks
            arenaBlocks[i] = ab;
            //then place it in the world
            PlaceArenaBlock(ab, i);
        }
    }
    public void PlaceArenaBlock(ArenaBlock ab, int pos, int steps = 1)
    {
        Vector3 dir = GetDictionaryFromPos(pos) * steps;
        Vector3 currenPos = ab.TheObject.transform.position;
        Vector3 newPos = currenPos + new Vector3
            (
                _arenaBlockWidth * dir.x,
                _arenaBlockHeight * dir.y,
                0               
            );
        ab.TheObject.transform.position = newPos;
    }
    private Vector3 GetDictionaryFromPos(int pos)
    {
        switch (pos)
        {
            case 0:
                return new Vector3(-1, 1, 0);
            case 1:
                return new Vector3(0, 1, 0);
            case 2:
                return new Vector3(1, 1, 0);
            case 3:
                return new Vector3(-1, 0, 0);
            case 4:
                return new Vector3(0, 0, 0);
            case 5:
                return new Vector3(1, 0, 0);
            case 6:
                return new Vector3(-1, -1, 0);
            case 7:
                return new Vector3(0, -1, 0);
            case 8:
                return new Vector3(1, -1, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }
    [Serializable]
    public class ArenaBlock
    {
        public int Position;
        public GameObject TheObject;
    }
    private int GetArenaPositionClosestToPlayer()
    {
        float distance = Mathf.Infinity;
        int champ = -1;
        for(int i = 0; i < 9; i++)
        {
            // calculater distance
            ArenaBlock tmpBlock = arenaBlocks[i];
            float tmpDistance = Vector3.Distance
                (
                    tmpBlock.TheObject.transform.position,
                    playerReference.transform.position
                );
            //check and update distance, and champ
            if(tmpDistance < distance)
            {
                distance = tmpDistance;
                champ = tmpBlock.Position;
            }
        }
        return champ;
    }
    /*
     * <summary>
     * update the arenaBlock according to players position
     */
    private void UpdateArenaModel()
    {
        switch (PlayerArenaBlockPosition)
        {
            case 0:
                /*
                 * move form 4 to 0
                 * [0][1][2] move [8][5][2] update [0][1][2]
                 * [3][4][5]      [7][0][1]        [3][4][5]
                 * [6][7][8]      [6][3][4]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[2], 0, 1);
                PlaceArenaBlock(arenaBlocks[6], 0, 1);
                PlaceArenaBlock(arenaBlocks[5], 0, 2);
                PlaceArenaBlock(arenaBlocks[7], 0, 2);
                PlaceArenaBlock(arenaBlocks[8], 0, 3);
                //update arenaBlocks position
                arenaBlocks[8].Position = 0;
                arenaBlocks[5].Position = 1;
                //arenaBlocks[2].Position = 2;
                arenaBlocks[7].Position = 3;
                arenaBlocks[0].Position = 4;
                arenaBlocks[1].Position = 5;
                //arenaBlocks[6].Position = 6;
                arenaBlocks[3].Position = 7;
                arenaBlocks[4].Position = 8;
                break;
            case 1:
                /*
                 * move form 4 to 1
                 * [0][1][2] move [6][7][8] update [0][1][2]
                 * [3][4][5]      [0][1][2]        [3][4][5]
                 * [6][7][8]      [3][4][5]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[6], 1, 3);
                PlaceArenaBlock(arenaBlocks[7], 1, 3);
                PlaceArenaBlock(arenaBlocks[8], 1, 3);
                //update arenaBlocks position
                arenaBlocks[6].Position = 0;
                arenaBlocks[7].Position = 1;
                arenaBlocks[8].Position = 2;
                arenaBlocks[0].Position = 3;
                arenaBlocks[1].Position = 4;
                arenaBlocks[2].Position = 5;
                arenaBlocks[3].Position = 6;
                arenaBlocks[4].Position = 7;
                arenaBlocks[5].Position = 8;
                break;
            case 2:
                /*
                 * move form 4 to 2
                 * [0][1][2] move [0][3][6] update [0][1][2]
                 * [3][4][5]      [1][2][7]        [3][4][5]
                 * [6][7][8]      [4][5][8]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[0], 2, 1);
                PlaceArenaBlock(arenaBlocks[8], 2, 1);
                PlaceArenaBlock(arenaBlocks[3], 2, 2);
                PlaceArenaBlock(arenaBlocks[7], 2, 2);
                PlaceArenaBlock(arenaBlocks[6], 2, 3);
                //update arenaBlocks position
                //arenaBlocks[0].Position = 0;
                arenaBlocks[3].Position = 1;
                arenaBlocks[6].Position = 2;
                arenaBlocks[1].Position = 3;
                arenaBlocks[2].Position = 4;
                arenaBlocks[7].Position = 5;
                arenaBlocks[4].Position = 6;
                arenaBlocks[5].Position = 7;
                //arenaBlocks[8].Position = 8;
                break;
            case 3:
                /*
                 * move form 4 to 3
                 * [0][1][2] move [2][0][1] update [0][1][2]
                 * [3][4][5]      [5][3][4]        [3][4][5]
                 * [6][7][8]      [8][6][7]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[2], 3, 3);
                PlaceArenaBlock(arenaBlocks[5], 3, 3);
                PlaceArenaBlock(arenaBlocks[8], 3 , 3);
                //update arenaBlocks position
                arenaBlocks[2].Position = 0;
                arenaBlocks[0].Position = 1;
                arenaBlocks[1].Position = 2;
                arenaBlocks[5].Position = 3;
                arenaBlocks[3].Position = 4;
                arenaBlocks[4].Position = 5;
                arenaBlocks[8].Position = 6;
                arenaBlocks[6].Position = 7;
                arenaBlocks[7].Position = 8;
                break;
            case 4:
                /*
                 * move in 4
                 */
                break;
            case 5:
                /*
                 * move form 4 to 5
                 * [0][1][2] move [1][2][0] update [0][1][2]
                 * [3][4][5]      [4][5][3]        [3][4][5]
                 * [6][7][8]      [7][8][6]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[0], 5, 3);
                PlaceArenaBlock(arenaBlocks[3], 5, 3);
                PlaceArenaBlock(arenaBlocks[6], 5, 3);
                //update arenaBlocks position
                arenaBlocks[1].Position = 0;
                arenaBlocks[2].Position = 1;
                arenaBlocks[0].Position = 2;
                arenaBlocks[4].Position = 3;
                arenaBlocks[5].Position = 4;
                arenaBlocks[3].Position = 5;
                arenaBlocks[7].Position = 6;
                arenaBlocks[8].Position = 7;
                arenaBlocks[6].Position = 8;
                break;
            case 6:
                /*
                 * move form 4 to 6
                 * [0][1][2] move [0][3][4] update [0][1][2]
                 * [3][4][5]      [1][6][7]        [3][4][5]
                 * [6][7][8]      [2][5][8]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[0], 6, 1);
                PlaceArenaBlock(arenaBlocks[8], 6, 1);
                PlaceArenaBlock(arenaBlocks[1], 6, 2);
                PlaceArenaBlock(arenaBlocks[5], 6, 2);
                PlaceArenaBlock(arenaBlocks[2], 6, 3);
                //update arenaBlocks position
                arenaBlocks[0].Position = 0;
                arenaBlocks[3].Position = 1;
                arenaBlocks[4].Position = 2;
                arenaBlocks[1].Position = 3;
                arenaBlocks[6].Position = 4;
                arenaBlocks[7].Position = 5;
                arenaBlocks[2].Position = 6;
                arenaBlocks[5].Position = 7;
                //arenaBlocks[8].Position = 8;
                break;
            case 7:
                /*
                 * move form 4 to 7
                 * [0][1][2] move [3][4][5] update [0][1][2]
                 * [3][4][5]      [6][7][8]        [3][4][5]
                 * [6][7][8]      [0][1][2]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[0], 7, 3);
                PlaceArenaBlock(arenaBlocks[1], 7, 3);
                PlaceArenaBlock(arenaBlocks[2], 7, 3);
                //update arenaBlocks position
                arenaBlocks[3].Position = 0;
                arenaBlocks[4].Position = 1;
                arenaBlocks[5].Position = 2;
                arenaBlocks[6].Position = 3;
                arenaBlocks[7].Position = 4;
                arenaBlocks[8].Position = 5;
                arenaBlocks[0].Position = 6;
                arenaBlocks[1].Position = 7;
                arenaBlocks[2].Position = 8;
                break;
            case 8:
                /*
                 * move form 4 to 8
                 * [0][1][2] move [4][5][2] update [0][1][2]
                 * [3][4][5]      [7][8][1]        [3][4][5]
                 * [6][7][8]      [6][3][0]        [6][7][8]
                 */
                PlaceArenaBlock(arenaBlocks[2], 8, 1);
                PlaceArenaBlock(arenaBlocks[6], 8, 1);
                PlaceArenaBlock(arenaBlocks[1], 8, 2);
                PlaceArenaBlock(arenaBlocks[3], 8, 2);
                PlaceArenaBlock(arenaBlocks[0], 8, 3);
                //update arenaBlocks position
                arenaBlocks[4].Position = 0;
                arenaBlocks[5].Position = 1;
                //arenaBlocks[2].Position = 2;
                arenaBlocks[7].Position = 3;
                arenaBlocks[8].Position = 4;
                arenaBlocks[1].Position = 5;
                //arenaBlocks[6].Position = 6;
                arenaBlocks[3].Position = 7;
                arenaBlocks[0].Position = 8;
                break;
            default:
                break;
        }
        //sort
        arenaBlocks.Sort((x, y) => x.Position.CompareTo(y.Position));
    }
}
