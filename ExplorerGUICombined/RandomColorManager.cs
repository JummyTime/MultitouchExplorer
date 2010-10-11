using System.Collections.Generic;
using System.Windows.Media;

namespace ExplorerGUICombined
{
    public static class RandomColorManager
    {
        private static List<Color> randomColors;
        private static int currentColor;

        public static Color getRandomColor()
        {
            if(randomColors == null)
            {
                randomColors = new List<Color>();
                randomColors.Add(Colors.AliceBlue);
                randomColors.Add(Colors.Aqua);
                randomColors.Add(Colors.Aquamarine);
                randomColors.Add(Colors.Azure);
                randomColors.Add(Colors.BlueViolet);
                randomColors.Add(Colors.Brown);
                currentColor = 0;
            }

            currentColor++;
            if(currentColor >= randomColors.Count)
            {
                currentColor = 0;
            } 

            return randomColors[currentColor];

        }

    }
}
