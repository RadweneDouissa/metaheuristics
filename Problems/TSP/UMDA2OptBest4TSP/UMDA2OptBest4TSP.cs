using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class UMDA2OptBest4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		protected double popFactor = 0.03;
		protected double truncFactor = 0.4;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberCities);
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA2OptBest4TSP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			umda.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "UMDA with 2-opt (best improvement) local search for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.EDA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}

		public void UpdateParameters(double[] parameters)
		{
			timePenalty = (int) parameters[0];
			popFactor = parameters[1];
			truncFactor = parameters[2];
		}
	}
}
