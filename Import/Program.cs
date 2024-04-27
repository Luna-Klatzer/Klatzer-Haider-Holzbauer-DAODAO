using Import;
using System;

var countryDao = new CountryDaoCSV("./Data/Countries_1960-2023.csv");
await countryDao.GetAllAsync();
Console.WriteLine();
