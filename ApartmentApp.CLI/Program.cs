// See https://aka.ms/new-console-template for more information

using ApartmentApp.CLI;

var apartment = new Apartment();

try
{
    Console.WriteLine(Environment.CurrentManagedThreadId);
    await Task.Run(() => { apartment.Area = 111; });
}
catch (CantModifyValueInOtherThread e)
{
    Console.WriteLine("Error occured!");
}
finally
{
    //apartment.Area = 111;
    Console.WriteLine(apartment.Area);
}
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
