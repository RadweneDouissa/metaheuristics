using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MABL2OptBest42SP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 250;
		protected double popSize = 2;
		protected double mutProbability = 0.2;
		

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			
			// Setting the parameters of the MA for this instance of the problem.
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberItems - 1;
			}
			DiscreteMA memetic = new DiscreteMABL2OptBest42SP(instance, (int)popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			memetic.Run(timeLimit - (int)timePenalty);
			int[,] coordinates = TwoSPUtils.BLCoordinates(instance, memetic.BestIndividual);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "MA using the BL heuristic with 2-opt (best improvement) local search for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.MA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}

		public void UpdateParameters(double[] parameters)
		{
			timePenalty = parameters[0];
			popSize = parameters[1];
			mutProbability = parameters[2];
		}		
	}
}
