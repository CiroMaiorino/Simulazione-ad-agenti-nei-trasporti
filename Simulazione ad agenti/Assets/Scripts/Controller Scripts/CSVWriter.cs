using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVWriter 
{
    GameControl gameControl;
    String path;
    public CSVWriter(GameControl gameControl, String path)
    {
        this.gameControl = gameControl;
        this.path = path;
    }

    public void createFile()
    {
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Agenti Totali;Agenti Contagiosi; Agenti Infetti; Agenti Sani");

        File.AppendAllText(path, csvContent.ToString());

    }
    public void writeStat()
    {
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine(gameControl.ATot+";"+gameControl.ACont+";"+gameControl.AInf+";"+gameControl.AH);

        File.AppendAllText(path, csvContent.ToString());

    }
}
