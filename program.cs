using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCode
{
	class Program
	{
		static string[] parkingGarage = new string[100]; 

		static void Main(string[] args)
		{
			bool run = true; 
			while (run)
			{
				// Display meny
				Console.Clear();
				Console.WriteLine("Menu: ");
				Console.WriteLine("1. Park car");
				Console.WriteLine("2. Park an MC");
				Console.WriteLine("3. Search for car");
				Console.WriteLine("4. Move vehicle");
				Console.WriteLine("5. Remove vehicle");
				Console.WriteLine("6. See vehicles");
				int menuChoice;
				Console.Write("Option: ");
				string input = Console.ReadLine();
				bool validMenuChoice = int.TryParse(input, out menuChoice);
				Console.WriteLine(validMenuChoice);
				Console.WriteLine(menuChoice);

				if (validMenuChoice)
				{
					Console.Clear();
					switch (menuChoice)
					{
						case 1:
							CarEnterGarage();
							break;

						case 2:
							McEnterGarage();
							break;

						case 3:
							SearchVehicle();
							break;

						case 4:
							MoveVehicle();
							break;

						case 5:
							RemoveVehicle();
							break;

						case 6:
							SeeVehicles();
							break;

						default:
							break;
					}
				}
			}

		}

		// Method: Park a car (from case 1 from switch)

		public static void CarEnterGarage()
		{
			Console.WriteLine("Please enter the registration number (eg. abc123): "); //Kan ändra här till "to park a car"
			string regNum = Console.ReadLine().ToUpper();

			if (IsRegistrationNumberValid(regNum) && !SearchRegNum(regNum)) // Har "regNum här för att det är specifikt till en bil

			{
				int myEmptySpot = IsNull();
				if (myEmptySpot >= 0)
				{
					parkingGarage[myEmptySpot] = "CAR:" + regNum;
					Console.WriteLine("Please park the car at {0}", myEmptySpot + 1);
					Console.ReadKey();
				}
				else
				{
					Console.WriteLine("The CAR PARK is full.");
					Console.WriteLine("Press a key to continue!");
					Console.ReadKey();
				}

			}
			else
			{
				Console.WriteLine("The registration number must be between 4-10 characters.");
				Console.WriteLine("You are already parked.");
				Console.ReadLine();
			}
		}

		// Method: Is registration number valid

		public static bool IsRegistrationNumberValid(string regNum)
		{
			if (regNum.Length <= 10 && regNum.Length >= 4)
			{
				return (true);
			}
			return (false);
		}

		// Mehtod: Park a MC (from case 2 in switch)

		public static void McEnterGarage()
		{
			Console.WriteLine("Please enter the registration number (eg. abc123): "); //kan ändra här till "to park a motorcycle"
			string mcRegNum = Console.ReadLine().ToUpper();
			if (IsRegistrationNumberValid(mcRegNum) && !SearchRegNum(mcRegNum)) // Har mcRegNum här för att det ska vara specifikt för en MC.
			{
				if (HasMC() >= 0)
				{
					int mySpot = HasMC();
					parkingGarage[mySpot] = parkingGarage[mySpot] + " | " + mcRegNum;
					Console.WriteLine($"We did find a spot on: {mySpot + 1}, and now your: {mcRegNum}, is parked there.");
					Console.WriteLine("Press a key to continue!");
					Console.ReadKey();
				}
				else if (IsNull() >= 0)
				{
					int mySpot = IsNull();
					parkingGarage[mySpot] = "MC:" + mcRegNum;
					Console.WriteLine($"We did find a spot on: {mySpot + 1}, and now your: {mcRegNum}, is parked there.");
					Console.WriteLine("Press a key to continue!");
					Console.ReadKey();
				}
			}
			else
			{
				Console.WriteLine("Either you did not use the correct format (a-z/A-Z/0-9) or your vehicle already is parked here.");
				Console.WriteLine("Press a key to continue!");
				Console.ReadKey();
			}
		}

		// Method: Identify vehicle type based on registration number
		public static string IdentifyVehicleType(string RegNum)
		{
			int parkingSpot = Array.FindIndex(parkingGarage, row => row.Contains(RegNum));
			string typeOfVehicle;

			if (parkingGarage[parkingSpot].Contains("CAR:"))
			{
				typeOfVehicle = "CAR";
				return typeOfVehicle;
			}
			else if (parkingGarage[parkingSpot].Contains("MC:"))
			{
				typeOfVehicle = "MC";
				return typeOfVehicle;
			}
			return "Error";
		}

		// Method: Find empty index in array

		static int FindIndex(string userInput, out int index)
		{
			for (int i = 0; i < parkingGarage.Length; i++)
			{
				if (parkingGarage[i] == null)
				{
					continue;
				}
				else if (parkingGarage[i] == "CAR:" + userInput || parkingGarage[i] == "MC:" + userInput || parkingGarage[i].Contains("MC:" + userInput))
				{
					index = i;
					return index;
				}
			}
			index = 0;
			return index;
		}

		// Method: Search for a vehicle (from case 3 in switch)

		static void SearchVehicle()
		{
			Console.WriteLine("Please enter your registration number: ");
			string userInput = Console.ReadLine().ToUpper();

			if (SearchRegNum(userInput) || SearchRegMC(userInput))
			{
				int index = FindIndex(userInput, out index);
				Console.WriteLine("The vehicle is parked at {0}", index + 1);
				Console.ReadKey();
			}
			else
			{
				Console.WriteLine("Sorry, we cant find your vehicle");
				Console.ReadKey();
			}

		}

		// Method: search for regNum (car)
		static bool SearchRegNum(string userInput)
		{
			for (int i = 0; i < parkingGarage.Length; i++)
			{
				if (parkingGarage[i] == null)
				{
					continue;

				}
				else if (parkingGarage[i] == "CAR:" + userInput || parkingGarage[i] == "MC:" + userInput)
				{
					return true;
				}


			}

			return false;
		}

		// Method: for search regNum (MC)
		static bool SearchRegMC(string userInput)
		{
			for (int i = 0; i < parkingGarage.Length; i++)
			{
				if (parkingGarage[i] == null)
				{
					continue;
				}
				else if (parkingGarage[i] == "MC:" + userInput || parkingGarage[i].Contains("MC:" + userInput))
				{
					return true;
				}
			}
			return false;
		}

		// Method: choose what type of vehicle to move (from case 4 in switch)

		public static void MoveVehicle()
		{
			Console.WriteLine("Do you want to move a car or a MC?: ");
			Console.WriteLine("1. Car");
			Console.WriteLine("2. MC");
			int userInput = int.Parse(Console.ReadLine());

			if (userInput == 1)
			{
				MoveCar();

			}
			else if (userInput == 2)
			{
				MoveMc();
			}
			else
			{
				Console.WriteLine("Press 1 or 2, please try again.");
				Console.ReadKey();
			}

		}

		// Method: moving a car

		public static void MoveCar()//Kolla rad 303
		{
			Console.WriteLine("To move the vehicle, please enter the registration number: ");
			string vehicleToMove = Console.ReadLine().ToUpper();

			if (SearchRegNum(vehicleToMove))
			{
				int index = FindIndex(vehicleToMove, out index);
				Console.WriteLine("Please enter a new parking spot (1-100): ");
				int newParkingSpot = int.Parse(Console.ReadLine());
				newParkingSpot--;

				if (parkingGarage[newParkingSpot] == null)
				{
					
					parkingGarage[newParkingSpot] = "CAR:" + vehicleToMove; //Bil flyttas till ny p-plats
					parkingGarage[index] = null; //Här tar vi bort bilen från den gamla platsen
					Console.WriteLine("We have now moved the vehicle from {0} to {1}", index + 1, newParkingSpot + 1);
					Console.ReadLine();


				}
				else
				{
					Console.WriteLine("The new spot is occupied, please try another one.");
					Console.ReadLine();
				}

			}
		}

		// Method: moving a mc

		public static void MoveMc()
		{
			Console.WriteLine("To move the vehicle, please enter the registration number: ");
			string vehicleToMove = Console.ReadLine().ToUpper();
			string seperator = " | MC:";

			if (SearchRegMC(vehicleToMove))
			{
				int index = FindIndex(vehicleToMove, out index);
				Console.WriteLine("Please enter a new parking spot (1-100): ");
				int newParkingSpot = int.Parse(Console.ReadLine());
				newParkingSpot--;

				if (parkingGarage[newParkingSpot] == null)
				{
					{
						if (parkingGarage[index] == "MC:" + vehicleToMove)
						{
							parkingGarage[newParkingSpot] = "MC:" + vehicleToMove;

							parkingGarage[index] = null;

                            Console.WriteLine("The MC is moved from {0} to spot {1}", index +1, newParkingSpot +1);
							Console.ReadKey();
						}
						else
						{
							string[] mcSplit = parkingGarage[index].Split(" | ");
							if (mcSplit[0] == "MC:" + vehicleToMove)
							{
								parkingGarage[newParkingSpot] = mcSplit[0];
								parkingGarage[index] = mcSplit[1];
							}
							else if (mcSplit[0] == "MC:" + vehicleToMove)
							{
								parkingGarage[newParkingSpot] = mcSplit[1];
								parkingGarage[index] = mcSplit[0];
							}
						}
					}
				}

				else if (parkingGarage[newParkingSpot].Contains(" | "))
				{
					Console.WriteLine("This spot is occupied, please choose another.");
				}
				else if (parkingGarage[newParkingSpot].Contains("MC:"))
				{
					if (parkingGarage[index] == "MC:" + vehicleToMove)
					{

						string temp = string.Join(seperator, parkingGarage[newParkingSpot], vehicleToMove);
						parkingGarage[newParkingSpot] = temp;
						parkingGarage[index] = null;
					}
					else
					{
						string[] mcSplit = parkingGarage[index].Split(" | ");

						if (mcSplit[0] == "MC:" + vehicleToMove)
						{
							parkingGarage[index] = mcSplit[1];
							string temp = string.Join(seperator, parkingGarage[newParkingSpot], vehicleToMove);
							parkingGarage[newParkingSpot] = temp;
						}
						else
						{
							parkingGarage[index] = mcSplit[0];
							string temp = string.Join(seperator, parkingGarage[newParkingSpot], vehicleToMove);
							parkingGarage[newParkingSpot] = temp;
						}


					}
					Console.WriteLine("We have moved your vehicle from {0}, to {1}.", index + 1, newParkingSpot + 1);
					Console.ReadLine();

				}
				else
				{
					if (parkingGarage[index].Contains(" | "))
					{
						string[] mcSplit = parkingGarage[index].Split(" | ");
						if (mcSplit[0] == "MC:" + vehicleToMove)
						{
							parkingGarage[index] = mcSplit[1];
							parkingGarage[newParkingSpot] = parkingGarage[index];
						}
						else
						{
							parkingGarage[index] = mcSplit[0];
							parkingGarage[newParkingSpot] = parkingGarage[index];
						}
					}
					else
					{
						parkingGarage[newParkingSpot] = parkingGarage[index];
						parkingGarage[index] = null;
					}
					Console.WriteLine("We have moved your vehicle from {0}, to {1}.", index, newParkingSpot);
					Console.ReadLine();
				}

			}
		}



		// Method: Remove vehicle from parking (from case 5 in switch)

		public static void RemoveVehicle()
		{
			Console.WriteLine("To remove your vehicle, please enter your registration number: ");
			string vehicleToRemove = Console.ReadLine().ToUpper();

			if (SearchRegNum(vehicleToRemove))
			{
				int index = FindIndex(vehicleToRemove, out index);

				if (parkingGarage[index] == "CAR:" + vehicleToRemove)
				{
					parkingGarage[index] = null;
					Console.WriteLine("We have now removed the vehicle {0}", vehicleToRemove);
					Console.ReadLine();
				}
				else if (parkingGarage[index].Contains("MC:" + vehicleToRemove))
				{
					if (parkingGarage[index].Contains(" | "))
					{
						string[] mcSplit = parkingGarage[index].Split(" | ");

						if (mcSplit[0] == "MC:" + vehicleToRemove)
						{
							parkingGarage[index] = mcSplit[1];
						}
						else
						{
							parkingGarage[index] = mcSplit[0];
						}
						Console.WriteLine("We have now removed the vehicle {0}", vehicleToRemove);
						Console.ReadKey();
					}
					else
					{
						parkingGarage[index] = null;
					}
					Console.WriteLine("We have now removed the vehicle {0}", vehicleToRemove);
					Console.ReadKey();
				}

			}
			else
			{
				Console.WriteLine("Sorry, vehicle is not here.");
				Console.ReadKey();
			}


		}
		
		// Method: Overview of all parked vehicles
		static void SeeVehicles()
		{
			foreach (var vehicle in parkingGarage)
			{
				int count = 1;
				if (vehicle == null)
				{
					Console.WriteLine("{0} Empty", count);
					count++;
				}
				else
				{
					Console.WriteLine("{0}: {1}", count, vehicle);
					count++;
				}
				
			}
			Console.ReadKey();

		}
		static int HasMC()
		{
			for (int i = 0; i < parkingGarage.Length; i++)
			{
				if (parkingGarage[i] != null && !parkingGarage[i].Contains("CAR:") && !parkingGarage[i].Contains("|"))
				{
					return i;
				}
			}
			return -1;
		}
		
		//Method: checks the index of first SOLO MC

		static int IsNull()
		{
			for (int i = 0; i < parkingGarage.Length; i++)
			{
				if (parkingGarage[i] == null)
				{
					return i;
				}
			}
			return -1;
		}
		
		//Methof: Checks the index of first NULL
		static bool IsFull(int userInput)
		{
			if (parkingGarage[userInput] != null)
			{
				if (parkingGarage[userInput].StartsWith("CAR:") || parkingGarage[userInput].Contains("|"))
				{
					return true;
				}
			}
			return false;
		}

	}
}
