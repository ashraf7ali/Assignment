using System.IO;
using System.Linq;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            // runs loop for number of provided input files
            for (int i = 1; i <= 3; i++)
            {
                Individuals.clearStaticChecks();
                routine(i);
                
            }

            Console.WriteLine("Done!!");
            Console.ReadKey();
        }

        static void routine(int round)
        {
            // setting the file path, add .. at the begining while in development environment
            string filePath = $@".\Input\accountDataInput{round}.csv";
            string previousFilePath = $@".\Output\Output{round - 1}.csv";
            string outFilePath = $@".\Output\Output{round}.csv";

            // 
            var individuals = new List<Individuals>();
            //var existingInd = new List<Individuals>();
            IDictionary<string, Individuals> exisInd = new Dictionary<string, Individuals>();

    

            // Output file 
            if (File.Exists(previousFilePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(previousFilePath))
                    {
                        string? rawRecord = reader.ReadLine();
                        while ((rawRecord = reader.ReadLine()) != null)
                        {
                            Individuals ind = new Individuals(rawRecord);
                            exisInd[ind.EmpId] = ind;
                            //existingInd.Add(ind);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }

            // Input file
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? rawRecord = reader.ReadLine();
                    while ((rawRecord = reader.ReadLine()) != null)
                    {
                        var ind = new Individuals(rawRecord);
                        if (exisInd.ContainsKey(ind.EmpId))
                        {
                            var existingIndividual = exisInd[ind.EmpId];
                            /*ind.FirstName = existingIndividual.FirstName;
                            ind.LastName = existingIndividual.LastName;
                            ind.PhoneNumber = existingIndividual.PhoneNumber;*/
                            ind.Email = existingIndividual.Email;
                            ind.AccName = existingIndividual.AccName;
                            individuals.Add(ind);
                        }
                        else
                        {

                            individuals.Add(ind);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            // Write to output file
            string data = "first_name,last_name,phone,employee_id,applicant,email,account_name\n";
            for (int i = 0; i < individuals.Count; i++)
            {
                data += individuals[i] + "\n";
            }
            File.WriteAllText(outFilePath, data);
        }
    }
}