using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using EverHealth;

namespace EverHealth.ViewModels
{
    class Calculations
    {
        private double _bmi;
        public double BMI
        {
            get { return _bmi; }
            set { _bmi = value; }
        }
        public void BMICalc(double weight, double height)
        {
            double bmi =Math.Round(weight / (height * height), 2);
            BMI = bmi;
        }
        public string bmiCategory(double bmi)
        {
            string bmiCat;
            if (bmi <= 18.50)
            {
                bmiCat = "underweight";
            }
            else if (bmi > 18.50 && bmi <= 25.00)
            {
                bmiCat = "normal";
            }
            else if (bmi > 25.00 && bmi <= 30.00)
            {
                bmiCat = "overweight";
            }
            else if (bmi > 30.00 && bmi <= 35.00)
            {
                bmiCat = "obese";
            }
            else if (bmi > 35.00)
            {
                bmiCat = "severely obese";
            }
            else
            {
                bmiCat = "ERROR";
            }
            return bmiCat;
        }






        private double _bmr;
        public double BMR
        {
            get { return _bmr; }
            set { _bmr = value; }
        }
        public void BMRCalc(double weight, double height, double age, string gender)
        {
            double bmr;
            if (gender == "male")
            {
                bmr = 66 + (13.7 * weight) + (5 * (height * 100)) - (6.8 * age);
            }
            else if (gender == "female")
            {
                bmr = 655 + (9.6 * weight) + (1.8 * (height * 100)) - (4.7 * age);
            }
            else
            {
                bmr = 0.00;
            }
            BMR = bmr;
            
        }





        private double _hbe;
        public double HBE
        {
            get { return _hbe; }
            set { _hbe = value; }
        }
        public void HBECalc(double bmr, string level)
        {
            double hbe;
            if (level == "sedentary")
            {
                hbe = bmr * 1.2;
            }
            else if (level == "light")
            {
                hbe = bmr * 1.375;
            }
            else if (level == "moderate")
            {
                hbe = bmr * 1.55;
            }
            else if (level == "active")
            {
                hbe = bmr * 1.725;
            }
            else if (level == "extra")
            {
                hbe = bmr * 1.9;
            }
            else
            {
                hbe = bmr;
            }
            HBE = Math.Round(hbe, 2);
        }





        private double _leanBodyMass;
        public double LeanBodyMass
        {
            get { return _leanBodyMass; }
            set { _leanBodyMass = value; }
        }


        private double _fattyBodyMass;
        public double FattyBodyMass
        {
            get { return _fattyBodyMass; }
            set { _fattyBodyMass = value; }
        }

        private double _bfp;
        public double BFP
        {
            get { return _bfp; }
            set { _bfp = value; }
        }

        public void CalcLean(string gender, double wrist, double waist, double hip, double forearm, double weight)
        {
            double leanMass = 0;
            if (gender == "male")
            {
                double factor1 = (weight * 1.082) + 94.42;
                double factor2 = waist * 4.15;
                leanMass = factor1 - factor2;
            }
            else if (gender == "female"){
                double factor1 = (weight * 0.732) + 8.987;
                double factor2 = wrist / 3.14;
                double factor3 = waist * 0.157;
                double factor4 = hip * 0.249;
                double factor5 = forearm * 0.434;
                leanMass = factor1 + factor2 - factor3 - factor4 + factor5;
            }
            LeanBodyMass = Math.Round(leanMass, 2);
            CalcFatty(weight, leanMass);
        }
        private void CalcFatty(double weight, double leanMass)
        {
            double fattyMass = weight - leanMass;
            FattyBodyMass = Math.Round(fattyMass, 2);
            CalcBFP(fattyMass, weight);
        }
        private void CalcBFP(double fattyMass, double weight)
        {
            double bfp = (fattyMass * 100) / weight;
            if (bfp < 0)
            {
                bfp = 0;
            }
            else if (bfp > 100)
            {
                bfp = 100;
            }
            BFP = Math.Round(bfp, 2);
        }


        private double _waistToHIp;
        public double WaistToHip
        {
            get { return _waistToHIp; }
            set { _waistToHIp = value; }
        }

        public void CalcW2H(double waist, double hip)
        {
            double w2h = Math.Round(waist / hip, 2);
            WaistToHip = w2h;
        }
        

    }
}
