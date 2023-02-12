using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string image { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public Skill(string image, string name, string description)
    {
        this.image = image;
        this.name = name;
        this.description = description;
    }
}
