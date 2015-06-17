using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EverHealth.ViewModels;
//The System.Windows.MEdia is a programming libary. It contains the definitions for the SolidColorBrush class, amond other things.
//This means that I can use pre-defined functions within my programming, which saves time etc.
//For more on why I used programming libarys, see my report.
using System.Windows.Media;

namespace EverHealth
{
    public partial class MainPage : PhoneApplicationPage
    {
        //This initilizes the Calculations class. This is Where all the answers are stored as well as all the methods that handle calculations.
        Calculations Calcs = new Calculations();
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        ////////////////
        //////HOME//////
        ////////////////
        #region
        //These methods are called on the button presses.
        //They change the default item of the panorama to the relevent screen.
        //Inside a try/Catch to avoid null reference exceptions
        private void navHBE_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[4];
            }
            catch { }
        }

        private void navBMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[1];
            }
            catch { }
        }

        private void navBMR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[2];
            }
            catch { }
        }

        private void navBFP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[3];
            }
            catch { }
        }

        private void navW2H_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[5];
            }
            catch { }
        }
        #endregion

        ////////////////
        //////BMI///////
        ////////////////
        #region
        
        //The 2 methods within this region are called when any slider is changed. 
        //They set the relevant label to the value of the relavent slider
        #region
        private void bmiWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try catch statement to catch null reference exceptions
            try
            {
                //sets bmiWeightSliderValue to the slider value at 2 decimal places
                double bmiWeightSliderValue = Math.Round(bmiWeightSlider.Value, 2);
                //sets the label to the slider value
                bmiWeightSliderValueLbl.Text = "Current Weight = " + bmiWeightSliderValue;
                //Calls the bmiMainMethod
                bmiMainMethod();
            }
            catch { }

        }

        private void bmiHeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try catch statement to catch null reference exceptions
            try
            {
                //sets bmiHeightSliderValue to the slider value at 2 decimal places
                double bmiHeightSliderValue = Math.Round(bmiHeightSlider.Value, 2);
                //sets the label to the slider value
                bmiHeightSliderValueLbl.Text = "Current Height = " + bmiHeightSliderValue;
                //Calls bmiMainMethod.
                bmiMainMethod();
            }
            catch { }
        }
        #endregion

        //Handles most of the main code, including getting values, setting the labels and calling the calculation class. Called on slider value changed.
        private void bmiMainMethod()
        {
           //Initilizes the convertMeasurments class, which is called when any meausrement needs to be converted to kg or metres.
            convertMeasurments convert = new convertMeasurments();

            //weight
            //Sets a string to the method bmiWeightUnit. This controls what the weight is measured in.
            string weightMeasurement = bmiWeightUnit();
            //This stores the weight slider value to 2 dp in a variable called weightSliderValue.
            double weightSliderValue = Math.Round(bmiWeightSlider.Value, 2);
            //This stores the weight in a variable called weightKg. 
            //It sets this variable to the convertWeight method which requires the weightMeasurment and the slider value.
            double weightKg = convert.convertWeight(weightMeasurement, weightSliderValue);

            //height
            //Sets a string to the method bmiHeightUnit. This controls what the height is measured in.
            string heightMeasurement = bmiHeightUnit();
            //This stores the height slider value to 2 dp in a variable called heightSliderValue.
            double heightSliderValue = Math.Round(bmiHeightSlider.Value, 2);
            //This stores the height in a variable called heightKg. 
            //It sets this variable to the convertheight method which requires the heightMeasurment and the slider value.
            double heightMetres = convert.convertHeight(heightMeasurement, heightSliderValue);

            //Calls the method that calculates BMI.
            //It requires the weight in kg and height in metres.
            Calcs.BMICalc(weightKg, heightMetres);

            //Get's the bmi category (underweight etc)
            string bmiCat = Calcs.bmiCategory(Calcs.BMI);

            //Sets the labels on the screen to the relavent data.
            //It gets the answer from the BMI property in the Calcs instance.
            bmiAnswerLbl.Text = "Your BMI is: " + Calcs.BMI;
            bmiCategoryLbl.Text = "This is classed as " + bmiCat;

            //This calls the method that selects the color of the stack panels.
            //It has the bmiCat parsed to it seeing as this is what the color is based on.
            bmiSelectColors(bmiCat);
        }

        //This is the method that sets the backgrounds of the stack panels. 
        //It is called from bmiSelectColors().
        //It is parsed the SolidColorCrush variable called color which contains the color the panels should be set to.
        private void setBmiColor(SolidColorBrush color)
        {
            //These run through each stack panel setting the background color to the color variable, which is parsed in from the bmiSelectColor method.
            bmiWeightSliderPanel.Background = color;
            bmiHeightSliderPanel.Background = color;
            answerBmiStackpanel.Background = color;
            answerCategoryStackpanel.Background = color;
        }

        //This method sets some SolidColorBrush variables to new instances of the SolidColorBrush class. 
        //It then selects an approproate color and calls the setBmiColor method.
        private void bmiSelectColors(string bmiCat)
        {

            //Sets color brushes to an instance of the SolidColorBrush class.
            //The values are converted fromRGB to colors using the FromARGB method.
            SolidColorBrush blue = new SolidColorBrush(Color.FromArgb(255, 000, 096, 156));
            SolidColorBrush green = new SolidColorBrush(Color.FromArgb(255, 009, 087, 000));
            SolidColorBrush yellow = new SolidColorBrush(Color.FromArgb(255, 212, 206, 049));
            SolidColorBrush orange = new SolidColorBrush(Color.FromArgb(255, 171, 105, 000));
            SolidColorBrush red = new SolidColorBrush(Color.FromArgb(255, 171, 000, 000));
            SolidColorBrush black = new SolidColorBrush(Color.FromArgb(255, 000, 000, 000));

            //Selects color. It uses if statements to see that the category is, and then calls the method which sets the color.
            //It parses in the relevant color to said method.
            if (bmiCat == "underweight")
            {
                setBmiColor(blue);
            }
            else if (bmiCat == "normal")
            {
                setBmiColor(green);
            }
            else if (bmiCat == "overweight")
            {
                setBmiColor(yellow);
            }
            else if (bmiCat == "obese")
            {
                setBmiColor(orange);
            }
            else if (bmiCat == "severely obese")
            {
                setBmiColor(red);
            }
            else
            {
                setBmiColor(black);
            }
        }

        //Set slider maximum on radio button click.
        //All of these methods are called when the relevent radio button is clicked. 
        //They contain a try catch statement to avoid null reference errors,
        //Within these try catch statements, there is code that sets the relevent sliders maximum to a sensible value.
        #region
        //Weight radio buttons
        private void bmiRadBtnWeightKg_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiWeightSlider.Maximum = 317.00;
            }
            catch { }
        }
        private void bmiRadBtnWeightStone_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiWeightSlider.Maximum = 50.00;
            }
            catch { }
        }
        private void bmiRadBtnWeightPounds_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiWeightSlider.Maximum = 700.00;
            }
            catch { }
        }

        //height radio buttons
        private void bmiRadBtnHeightMetres_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiHeightSlider.Maximum = 2.50;
            }
            catch { }
        }
        private void bmiRadBtnHeightFeet_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiHeightSlider.Maximum = 8.00;
            }
            catch { }
        }
        private void bmiRadBtnHeightInches_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmiHeightSlider.Maximum = 96.00;
            }
            catch { }
        }
        #endregion

        //Methods to return the weight/height unit
        

        //These functions uses if statements to see which radio button is checked.
        //Once it finds out which radio button is checked, it sets the variable weightUnit or heightUnit to the relevant value.
        //This value is then used within converting the measurements.
        //This is procedural code because it can be called whenever.
        private string bmiWeightUnit()
        {
            string weightUnit;
            if (bmiRadBtnWeightPounds.IsChecked == true)
            {
                weightUnit = "pounds";
            }
            else if (bmiRadBtnWeightKg.IsChecked == true)
            {
                weightUnit = "kg";
            }
            else if (bmiRadBtnWeightStone.IsChecked == true)
            {
                weightUnit = "stone";
            }
            else
            {
                weightUnit = "ERROR";
            }
            return weightUnit;
        }
        private string bmiHeightUnit()
        {
            string heightUnit;
            if (bmiRadBtnHeightMetres.IsChecked == true)
            {
                heightUnit = "metres";
            }
            else if (bmiRadBtnHeightFeet.IsChecked == true)
            {
                heightUnit = "feet";
            }
            else if (bmiRadBtnHeightInches.IsChecked == true)
            {
                heightUnit = "inches";
            }
            else
            {
                heightUnit = "ERROR";
            }
            return heightUnit;
        }

        #endregion

        ////////////////
        //////BMR///////
        ////////////////
        #region

        //These methods are called when the slider values are changed.
        //these are proedural.
        private void bmrWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try catch statement to catch null reference exceptions
            try
            {
                //sets bmrWeightSliderValue to the slider value at 2 decimal places
                double bmrWeightSliderValue = Math.Round(bmrWeightSlider.Value, 2);
                //sets the label to the slider value
                bmrWeightSliderValueLbl.Text = "Current Weight = " + bmrWeightSliderValue;
                //Calls the main bmr method.
                bmrMainMethod();
            }
            catch { }
        }

        private void bmrHeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try catch statement to catch null reference exceptions
            try
            {
                //sets bmrHeightSliderValue to the slider value at 2 decimal places
                double bmrHeightSliderValue = Math.Round(bmrHeightSlider.Value, 2);
                //sets the label to the slider value
                bmrHeightSliderValueLbl.Text = "Current Height = " + bmrHeightSliderValue;
                //Calls the main bmr method.
                bmrMainMethod();
            }
            catch { }
        }

        private void bmrAgeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //try catch statement to catch null reference exceptions
            try
            {
                //sets bmiAgeSliderValue to the slider value, rounds down to avoid decimal places
                double bmrAgeSliderValue = Math.Floor(bmrAgeSlider.Value);
                //sets the label to the slider value
                bmrAgeSliderValueLbl.Text = "Current Age = " + bmrAgeSliderValue;
                //Calls the main bmr method.
                bmrMainMethod();
            }
            catch { }
        }

        //Set Slider maximums on radio button checks
        #region
        //Weight radio buttons
        private void bmrRadBtnWeightKg_Checked(object sender, RoutedEventArgs e)
        {
            //Uses a try catch statement to avoid null reference errors.
            try
            {
                bmrWeightSlider.Maximum = 317.00;
            }
            catch { }
        }
        private void bmrRadBtnWeightStone_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmrWeightSlider.Maximum = 50.00;
            }
            catch { }
        }
        private void bmrRadBtnWeightPounds_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmrWeightSlider.Maximum = 700.00;
            }
            catch { }
        }

        //Height radio buttons
        private void bmrRadBtnHeightMetres_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmrHeightSlider.Maximum = 2.50;
            }
            catch { }
        }
        private void bmrRadBtnHeightFeet_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmrHeightSlider.Maximum = 8.00;
            }
            catch { }
        }
        private void bmrRadBtnHeightInches_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                bmrHeightSlider.Maximum = 96.00;
            }
            catch { }
        }

        #endregion

        //Methods to return the weight/height unit

        //These functions uses if statements to see which radio button is checked.
        //Once it finds out which radio button is checked, it sets the variable weightUnit or heightUnit to the relevant value.
        //This value is then used within converting the measurements.
        //This is procedural code because it can be called whenever.
        #region
        private string bmrWeightUnit()
        {
            string weightUnit;
            if (bmrRadBtnWeightPounds.IsChecked == true)
            {
                weightUnit = "pounds";
            }
            else if (bmrRadBtnWeightKg.IsChecked == true)
            {
                weightUnit = "kg";
            }
            else if (bmrRadBtnWeightStone.IsChecked == true)
            {
                weightUnit = "stone";
            }
            else
            {
                weightUnit = "ERROR";
            }
            return weightUnit;
        }
        private string bmrHeightUnit()
        {
            string heightUnit;
            if (bmrRadBtnHeightMetres.IsChecked == true)
            {
                heightUnit = "metres";
            }
            else if (bmrRadBtnHeightFeet.IsChecked == true)
            {
                heightUnit = "feet";
            }
            else if (bmrRadBtnHeightInches.IsChecked == true)
            {
                heightUnit = "inches";
            }
            else
            {
                heightUnit = "ERROR";
            }
            return heightUnit;
        }
        #endregion

        
        //This is the main bmr method. It gets all the values and then parses them into the BMRCalc method.
        //It then sets the labels on the screen to the object.
        private void bmrMainMethod()
        {
            //this creates a new instane of the convertMeasurements class, which is used when conveting measurments to kg and metres.
            convertMeasurments convert = new convertMeasurments();

            //weight
            //Sets the weight measurement to the bmrweightunit method which is either stone, kg or pounds.
            string weightMeasurement = bmrWeightUnit();
            //Sets the variable weightSliderValue to the slider value at two d.p
            double weightSliderValue = Math.Round(bmrWeightSlider.Value, 2);
            //Sets the weightKg variable to the convertWeight method of the convertMeasurements class.
            //This has the weight unit and weight value parsed in.
            //It then returns the weight in kilograms to the variable.
            double weightKg = convert.convertWeight(weightMeasurement, weightSliderValue);

            //height
            string heightMeasurement = bmrHeightUnit();
            double heightSliderValue = Math.Round(bmrHeightSlider.Value, 2);
            double heightMetres = convert.convertHeight(heightMeasurement, heightSliderValue);

            //age
            double age = Math.Floor(bmrAgeSlider.Value);

            //gender
            string gender = String.Empty;
            //This is an if statement. This is a procedural technique.
            //Explanation of the use is in my full report.
            if (bmrRadBtnGenderMale.IsChecked == true)
            {
                gender = "male";
            }
            else if (bmrRadBtnGenderFemale.IsChecked == true)
            {
                gender = "female";
            }

            //Calling the method BMRCalc which requires the weight, height, age and gender.
            //This is procedural and a more in depth explanation is available in my report.
            //It is also object orientated seeing as it is a part of the Calculations class.
            Calcs.BMRCalc(weightKg, heightMetres, age, gender);

            //Set label to the property BMR of the Calculations class.
            bmrAnswerLbl.Text = "Your BMR is: " + Calcs.BMR;
        }
        #endregion


        ////////////////
        //////BFP///////
        ////////////////
        #region
        //On gender radio button checks
        #region

        //When the gender radio buttons are clicked, it calls the setStackOpacity method parsing a value of 0 or 100 to it.
        //This shows or hides the relevant stack panels for male/female body fat percentage calculation.
        private void bfpRadBtnGenderMale_Checked(object sender, RoutedEventArgs e)
        {
            setStackOpacity(0);
        }
        private void bfpRadBtnGenderFemale_Checked(object sender, RoutedEventArgs e)
        {
            setStackOpacity(100);
        }
        #endregion

        //On weight unit radio button checks
        #region
        private void bfpRadBtnWeightKg_Checked(object sender, RoutedEventArgs e)
        {
            //This calls a procedure which sets the body fat percentage slider maximums and minimums.
            //This is a key feature within procedural programming, for more detail se my report.
            bfpSliderMaxMins(317, 20, 150, 50, 150, 0);
            //This calls a procedure which sets the text at the end of the sliders to say whatever
            //is parsed in.
            bfpSliderUnits("Cm");
        }
        private void bfpRadBtnWeightPounds_Checked(object sender, RoutedEventArgs e)
        {
            bfpSliderMaxMins(700, 8, 70, 25, 70, 0);
            bfpSliderUnits("In");
        }
        #endregion

        //This is a procedure (returns no value) that sets the slider maximums and minimums.
        //This is a part of procedural programming and is useful because it means I do not need to type out the code within everytime the method is called, meaning it is easier to
        //manage code and fix errors if it is wrong seeing as there is one 'master copy'
        private void bfpSliderMaxMins(int maxWeight, int maxWrist, int maxWaist, int maxForearm, int maxHip, int min)
        {
            //This uses local variables (maxWeight etc) so I can use the variable names for other
            //relevent assignments, in other methods.
            try
            {
                bfpWeightSlider.Maximum = maxWeight;
                bfpWeightSlider.Minimum = min;
                bfpWristSlider.Maximum = maxWrist;
                bfpWaistSlider.Maximum = maxWaist;
                bfpForearmSlider.Maximum = maxForearm;
                bfpHipSlider.Maximum = maxHip;
            }
            catch { }
        }
        private void bfpSliderUnits(string unit)
        {
            try
            {
                bfpWaistUnit.Text = unit;
                bfpWristUnit.Text = unit;
                bfpHipUnit.Text = unit;
                bfpForearmUnit.Text = unit;
            }
            catch { }
        }
        //This method uses parameter parsing to make code easier to manage.
        //This is because it is easier to set each of the stackpanels opacity to the local variable opacity
        //than it is to write the code out twice each time setting it to a static int.
        private void setStackOpacity(int opacity)
        {
            //This value of opacity is parsed in when the method is called.
            //For more on why I did this see my report.
            bfpPanelForearm.Opacity = opacity;
            bfpPanelHip.Opacity = opacity;
            bfpPanelWrist.Opacity = opacity;
        }

        //Slider values changed
        #region
        private void bfpWeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //This uses the Math.Round pre-defined function, which is a part of procedural programming.
                //This is useful because it means I dont need to write my own method to round numbers down.
                //This saves time. For more on why pre-defined functions are useful, see my report.
                bfpWeightSliderValueLbl.Text = "Your weight is: " + Math.Round(bfpWeightSlider.Value, 2);
                bfpMainMethod();
            }
            catch { }
        }
        private void bfpWaistSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bfpMainMethod();
        }
        private void bfpWristSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bfpMainMethod();
        }
        private void bfpHipSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bfpMainMethod();
        }
        private void bfpForearmSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            bfpMainMethod();
        }
        #endregion

        //This method enables modularity within my programming, meaning I can just call it 5 times
        //rather than writing out all of the code within in 5 times. This saves times, among other benefits.
        //This is essentiall within procedural programming because it allows the program to be more easily maintained and written.
        private void bfpMainMethod()
        {
            convertMeasurments convert = new convertMeasurments();

            //Get gender
            string gender = "";
            try
            {
                if (bfpRadBtnGenderFemale.IsChecked == true)
                {
                    gender = "female";
                }
                else if (bfpRadBtnGenderMale.IsChecked == true)
                {
                    gender = "male";
                }
                else
                {
                    gender = "male";
                }
            }
            catch { }


            //Get weight unit, circumference unit.
            string weightUnit = "";
            string circUnit = "";
            try
            {
                if (bfpRadBtnWeightKg.IsChecked == true)
                {
                    weightUnit = "kg";
                    circUnit = "cm";
                }
                else if (bfpRadBtnWeightPounds.IsChecked == true)
                {
                    weightUnit = "pounds";
                    circUnit = "in";
                }
                else
                {
                    weightUnit = "";
                    circUnit = "";
                }
            }
            catch { }

            //Sets bfpWeightSliderVal to the weight slider value
            double bfpWeightSliderVal = 0;
            try
            {
                bfpWeightSliderVal = bfpWeightSlider.Value;
            }
            catch { }
            //Sets a variable called weight to the method convertWeightToPounds, which requires weight unit and the slider value. It then round it to two d.p.
            double weight = 0;
            weight = Math.Round(convert.weightToPounds(weightUnit, bfpWeightSliderVal), 2);


            //Sets the relevent variable to the method that converts circumference to inches, which requires the circumference unit and slidr value. Then rounds to 2 d.p
            double waist, hip, forearm, wrist;
            waist = hip = forearm = wrist = 0;
            try
            {
                waist = Math.Round(convert.circToInches(circUnit, bfpWaistSlider.Value), 2);
                hip = Math.Round(convert.circToInches(circUnit, bfpHipSlider.Value), 2);
                forearm = Math.Round(convert.circToInches(circUnit, bfpForearmSlider.Value), 2);
                wrist = Math.Round(convert.circToInches(circUnit, bfpWristSlider.Value), 2);
            }
            catch { }

            //Calls the calc lean method, which then calculates lean mass, then fatty mass and then ody fat percentage
            Calcs.CalcLean(gender, wrist, waist, hip, forearm, weight);

            //Sets labels to answers
            try
            {
                bfpLeanAnswerLbl.Text = "Lean body mass is " + Calcs.LeanBodyMass + " pounds.";
                bfpFatAnswerLbl.Text = "Fatty body mass is " + Calcs.FattyBodyMass + " pounds";
                bfpPercentageAnswerLbl.Text = "Body fat percentage is " + Calcs.BFP + "%";
            }
            catch { }

        }

        #endregion

        ////////////////
        //////HBE///////
        ////////////////
        #region
        private void calcBMRLbl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Calcs.BMR != 0)
                {
                    BMRAnswerLbl.Text = Convert.ToString(Calcs.BMR);
                }
                else
                {
                    EverHealthPanorama.DefaultItem = EverHealthPanorama.Items[2];
                }
            }
            catch { }
        }

        //Radio buttons checked
        private void hbeExLvlSedentry_Checked(object sender, RoutedEventArgs e)
        {
            hbeMainMethod("sedentary");
        }

        private void hbeExLvlLight_Checked(object sender, RoutedEventArgs e)
        {
            hbeMainMethod("light");
        }

        private void hbeExLvlModerate_Checked(object sender, RoutedEventArgs e)
        {
            hbeMainMethod("moderate");
        }

        private void hbeExLvlVery_Checked(object sender, RoutedEventArgs e)
        {
            hbeMainMethod("active");
        }

        private void hbeExLvlExtra_Checked(object sender, RoutedEventArgs e)
        {
            hbeMainMethod("extra");
        }

        private void hbeMainMethod(string level)
        {
            //Call calculate method
            Calcs.HBECalc(Calcs.BMR, level);

            //Set labels
            hbeAnswerLbl.Text = "You require " + Calcs.HBE + " calories a day!";
        }
        #endregion

        ////////////////
        //////W2H///////
        ////////////////
        #region
        //On sliders changed
        #region
        private void w2hWaistSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                w2hWaistSliderValueLbl.Text = "Current waist: " + Math.Round(w2hWaistSlider.Value, 2);
            }
            catch { }
            w2hMainMethod();
        }

        private void w2hHipSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                w2hHipSliderValueLbl.Text = "Current hip: " + Math.Round(w2hHipSlider.Value, 2);
            }
            catch { }
            w2hMainMethod();
        }

        #endregion

        private void w2hMainMethod()
        {
            //Get waist and hip values from slider, round to 2 dp
            double waist = 0;
            double hip = 0;
            try
            {
                waist = Math.Round(w2hWaistSlider.Value, 2);
                hip = Math.Round(w2hHipSlider.Value, 2);
            }
            catch { }

            //Calls the waist to hip calculation method, requires waist and hip values.
            Calcs.CalcW2H(waist, hip);

            //Set labels
            try
            {
                w2hLbl.Text = "Your waist to hip ratio is: " + Calcs.WaistToHip;
            }
            catch { }
            
        }
        #endregion

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            bmrShowPanel.DataContext = Calcs;
        }


    }



}