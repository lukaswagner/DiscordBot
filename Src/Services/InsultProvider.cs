using System;
using System.Collections.Generic;
using System.IO;

namespace DiscordBot
{
    public class InsultProvider
    {
        private List<string> _insults;
        private static Random _random = new Random();

        public string GetInsult()
        {
            int index= _random.Next(_insults.Count);
            return _insults[index];
        }

        public InsultProvider()
        {
            string insultString = File.ReadAllText("Resources/Insults/insults.txt");
            _insults = new List<string>(insultString.Split('\n'));
        }
    }
}
