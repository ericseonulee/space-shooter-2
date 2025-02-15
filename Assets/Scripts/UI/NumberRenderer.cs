﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumberRenderer : MonoBehaviour {
    public enum Align {
        LeftAlign,
        RightAlign
    }

    [SerializeField] public Align align = Align.LeftAlign;

    public List<SpriteNumber> numbers;

    public GameObject genaricNumber;

    public void RenderNumber(int aNum) {
        if (aNum < 0) {
            Debug.LogError("Can not submit negative numbers! yet!");
            Debug.Break();
            return;
        }
        //Get Lengths
        int length = aNum.ToString().Length;
        char[] strNum = aNum.ToString().ToCharArray();

        //If There is no numbers in the array, add one
        if (numbers.Count == 0) {
            GameObject temp = Instantiate(genaricNumber, this.transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = this.gameObject.transform;
            numbers.Add(temp.GetComponent<SpriteNumber>());

        }
        //Check to see if there is enough numbers to render the incoming number
        if (align == Align.LeftAlign) {
            for (int i = 0; i < length; i++) {
                if (length > numbers.Count) {
                    GameObject temp = Instantiate(genaricNumber, numbers[numbers.Count - 1].gameObject.transform.position + new Vector3(numbers[numbers.Count - 1].render.bounds.size.x, 0, 0), Quaternion.identity);
                    temp.transform.parent = gameObject.transform;
                    numbers.Add(temp.GetComponent<SpriteNumber>());
                }
            }
        }
        else if (align == Align.RightAlign) {
            for (int i = 0; i < length; i++) {
                if (length > numbers.Count) {
                    float previousNumberSize = numbers[numbers.Count - 1].render.bounds.size.x;

                    GameObject temp = Instantiate(genaricNumber, numbers[numbers.Count - 1].gameObject.transform.position, Quaternion.identity);

                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - previousNumberSize,
                                                                gameObject.transform.position.y,
                                                                gameObject.transform.position.z);
                    temp.transform.parent = gameObject.transform;
                    numbers.Add(temp.GetComponent<SpriteNumber>());
                }
            }
        }
        //Remove any unused numbers
        if (length < numbers.Count) {
            int howManToDelete = 0;
            for (int i = length; i < numbers.Count; i++) {
                DestroyImmediate(numbers[i].gameObject);
                howManToDelete++;
            }
            numbers.RemoveRange(length, howManToDelete);
        }

        //Finally Set the number to each number in the array
        for (int i = 0; i < numbers.Count; i++) {
            numbers[i].SetNumber(int.Parse(strNum[i].ToString()));
        }
    }
}
