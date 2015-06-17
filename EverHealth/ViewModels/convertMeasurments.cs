using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverHealth.ViewModels
{
    class convertMeasurments
    {
        //Convetrs the weight to kg. parses in the weight unit and sliderValue
        public double convertWeight(string weightUnit, double sliderValue)
        {
            double weightKg;
            switch (weightUnit)
            {
                case "kg":
                    weightKg = sliderValue;
                    break;
                case "stone":
                    weightKg = sliderValue / 0.15747;
                    break;
                case "pounds":
                    weightKg = sliderValue / 2.2046;
  
                    break;
                default:
                    weightKg = sliderValue;
                    break;
            }
            return weightKg;
        }

        //Convetrs the height to metres. parses in the height unit and sliderValue
        public double convertHeight(string heightUnit, double sliderValue)
        {
            double heightMetres;
            switch (heightUnit)
            {
                case "metres":
                    heightMetres = sliderValue;
                    break;
                case "inches":
                    heightMetres = sliderValue / 39.370;
                    break;
                case "feet":
                    heightMetres = sliderValue / 3.2808;
                    break;
                default:
                    heightMetres = sliderValue;
                    break;
            }
            return heightMetres;
        }

        //Converts weight to Pounds, used in body fat percetage calculation
        public double weightToPounds(string weightUnit, double weight)
        {
            double weightPounds;
            switch (weightUnit)
            {
                case "kg":
                    weightPounds = weight * 2.20462262;
                    break;
                case "pounds":
                    weightPounds = weight;
                    break;
                default:
                    weightPounds = weight;
                    break;
            }
            return weightPounds;
        }

        //Converts Circumference to Inches, used in body fat percetage calculation
        public double circToInches(string circUnit, double circumference)
        {
            double circInches = 0;
            switch (circUnit)
            {
                case "cm":
                    circInches = circumference * 0.39370079;
                    break;
                case "in":
                    circInches = circumference;
                    break;
                default:
                    circInches = circumference;
                    break;
            }
            return circInches;
        }
    }
}
