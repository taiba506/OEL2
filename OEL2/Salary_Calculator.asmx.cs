using System;
using System.Web.Services;

namespace OEL2
{
    /// <summary>
    /// Web service for calculating salaries of various employee types.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Salary_Calculator : System.Web.Services.WebService
    {
        /// <summary>
        /// Calculates monthly salary for full-time employees based on logged hours.
        /// </summary>
        [WebMethod]
        public string CalculateFullTimeSalary(double annualSalary, int weeklyHours, int workingWeeks, double loggedHours)
        {
            if (annualSalary <= 0 || weeklyHours <= 0 || workingWeeks <= 0 || loggedHours < 0)
                return "Error: Invalid input. All values must be positive.";

            double totalAnnualHours = weeklyHours * workingWeeks;
            double hourlyRate = annualSalary / totalAnnualHours;
            double salary = hourlyRate * loggedHours;

            return $"Hourly Rate: {hourlyRate:F2}, Calculated Salary: {salary:F2}";
        }

        /// <summary>
        /// Calculates salary for part-time employees with max hours validation.
        /// </summary>
        [WebMethod]
        public string CalculatePartTimeSalary(double hourlyRate, double totalHours)
        {
            const double maxHoursPerWeek = 25;

            if (hourlyRate <= 0 || totalHours < 0)
                return "Error: Invalid hourly rate or hours worked.";

            double avgWeeklyHours = totalHours / 4; // Assuming 4 weeks in a month
            double salary = hourlyRate * totalHours;

            if (avgWeeklyHours > maxHoursPerWeek)
                return $"Warning: Overtime detected. Salary: {salary:F2}";

            return $"Salary: {salary:F2}";
        }

        /// <summary>
        /// Calculates salary for contract employees.
        /// </summary>
        [WebMethod]
        public string CalculateContractSalary(bool isMilestoneBased, double milestonePayment, int milestonesCompleted, double fixedFee)
        {
            if (isMilestoneBased)
            {
                if (milestonePayment <= 0 || milestonesCompleted < 0)
                    return "Error: Invalid milestone payment or count.";
                return $"Salary: {(milestonePayment * milestonesCompleted):F2}";
            }
            else
            {
                if (fixedFee <= 0)
                    return "Error: Invalid fixed project fee.";
                return $"Salary: {fixedFee:F2}";
            }
        }

        /// <summary>
        /// Calculates salary for freelance workers based on task count and rates.
        /// </summary>
        [WebMethod]
        public string CalculateFreelanceSalary(string[] taskCategories, int[] taskCounts, double[] taskRates)
        {
            if (taskCategories == null || taskCounts == null || taskRates == null)
                return "Error: Null input arrays.";

            if (taskCategories.Length != taskCounts.Length || taskCounts.Length != taskRates.Length)
                return "Error: Input array lengths do not match.";

            double total = 0;

            for (int i = 0; i < taskCounts.Length; i++)
            {
                if (taskCounts[i] < 0 || taskRates[i] < 0)
                    return "Error: Task count and rates must be non-negative.";

                total += taskCounts[i] * taskRates[i];
            }

            return $"Total Freelance Salary: {total:F2}";
        }
    }
}
