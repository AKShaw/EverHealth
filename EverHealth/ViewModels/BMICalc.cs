using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverHealth.ViewModels
{
    class BMICalc
    {
        public void bmiCalculate(string heightUnit, string weightUnit, double heightSliderValue, double weightSliderValue)
        {
            double bmiheightMetres, bmiWeightKg;
            convertMeasurments unitConvert = new convertMeasurments();
            bmiWeightKg = unitConvert.convertWeight(weightUnit, weightSliderValue);
        }
    }
}
