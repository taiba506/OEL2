using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OEL2
{
    /// <summary>
    /// Summary description for Salary_Calculator
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Salary_Calculator : System.Web.Services.WebService
    {
        [WebMethod]
        public double CalculateFullTimeSalary(double annualSalary, int weeklyHours, int workingWeeks, double loggedHours)
        {
            if (annualSalary <= 0 || weeklyHours <= 0 || workingWeeks <= 0)
                return -1; // Invalid input

            double totalHours = weeklyHours * workingWeeks;
            double hourlyRate = annualSalary / totalHours;
            return hourlyRate * loggedHours;
        }

        [WebMethod]
        public string CalculatePartTimeSalary(double hourlyRate, double totalHours)
        {
            double maxHoursPerWeek = 25;
            double weeklyHours = totalHours / 4;
            if (weeklyHours > maxHoursPerWeek)
                return "Warning: Overtime. Salary = " + (hourlyRate * totalHours).ToString();

            return (hourlyRate * totalHours).ToString();
        }

        [WebMethod]
        public double CalculateContractSalary(bool isMilestoneBased, double milestonePayment, int milestonesCompleted, double fixedFee)
        {
            return isMilestoneBased ? milestonePayment * milestonesCompleted : fixedFee;
        }

        [WebMethod]
        public double CalculateFreelanceSalary(string[] taskCategories, int[] taskCounts, double[] taskRates)
        {
            if (taskCategories.Length != taskCounts.Length || taskCounts.Length != taskRates.Length)
                return -1;

            double total = 0;
            for (int i = 0; i < taskCategories.Length; i++)
            {
                total += taskCounts[i] * taskRates[i];
            }
            return total;
        }
    }
}
