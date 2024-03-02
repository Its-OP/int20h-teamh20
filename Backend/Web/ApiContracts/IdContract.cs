namespace backend.ApiContracts;

public class IdContract<T>
{
    public IdContract(T id)
    {
        Id = id;
    }

    public T Id { get; set; }
}