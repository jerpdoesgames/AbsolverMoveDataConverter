using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace absolverMoves
{
    class Program
    {
        private const string SOURCE_DIR = "../../data/";
        public static string storagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AbsolverMoves/");

        static void Main(string[] args)
        {
            List<dataObjects.moveData> moveList = new List<dataObjects.moveData>();

            dataObjects.sourceMoveList sourceMovesBarehand = new dataObjects.sourceMoveList();
            dataObjects.sourceMoveList sourceMovesSword = new dataObjects.sourceMoveList();

            string filePathBarehands = SOURCE_DIR + "barehands.json";
            
            if (File.Exists(filePathBarehands))
            {
                string sourceMovesString = File.ReadAllText(filePathBarehands);
                if (!string.IsNullOrEmpty(sourceMovesString))
                {
                    sourceMovesBarehand = new JavaScriptSerializer().Deserialize<dataObjects.sourceMoveList>(sourceMovesString);
                }
            }

            string filePathSword = SOURCE_DIR + "sword.json";

            if (File.Exists(filePathSword))
            {
                string sourceMovesString = File.ReadAllText(filePathSword);
                if (!string.IsNullOrEmpty(sourceMovesString))
                {
                    sourceMovesSword = new JavaScriptSerializer().Deserialize<dataObjects.sourceMoveList>(sourceMovesString);
                }
            }

            for (int i = 0; i < sourceMovesBarehand.moveList.Count; i++)
            {
                moveList.Add(new dataObjects.moveData(sourceMovesBarehand.moveList[i]));
            }

            for (int i = 0; i < sourceMovesSword.moveList.Count; i++)
            {
                moveList.Add(new dataObjects.moveData(sourceMovesSword.moveList[i]));
            }

            string fullOutput = new JavaScriptSerializer().Serialize(moveList);

            File.WriteAllText(storagePath + "absolverMoves.json", fullOutput);
        }
    }
}
