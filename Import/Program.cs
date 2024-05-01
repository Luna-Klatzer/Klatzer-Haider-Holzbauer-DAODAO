using Import;
using System;


var jsonDAO = new HappinessIndexDaoJSON("./Data/WorldHappinessIndex_2023.json");
var countriesJson = await jsonDAO.GetAllAsync();

var csvDAO = new HappinessIndexDaoCSV("./Data/Countries_1960-2023.csv", "./Data/WorldHappinessIndex_2005-2020.csv");
var countriesCsv = await csvDAO.GetAllAsync();
Console.WriteLine();
