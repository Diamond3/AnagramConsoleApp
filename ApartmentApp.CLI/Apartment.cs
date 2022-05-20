namespace ApartmentApp.CLI;

public class Apartment
{
    private readonly int _mainThreadId;
    private int _area = 10;
    public int Area
    {
        get => _area;
        set
        {
            if (_mainThreadId != Environment.CurrentManagedThreadId)
            {
                Console.WriteLine(Environment.CurrentManagedThreadId);
                throw new CantModifyValueInOtherThread();
            }
            _area = value;
        }
    }

    public Apartment()
    {
        _mainThreadId = Environment.CurrentManagedThreadId;
    }
}

public class CantModifyValueInOtherThread : Exception
{
}