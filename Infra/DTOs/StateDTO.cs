namespace Infra.DTOs
{
    public enum StateDTO
    {
        Active,
        Delete
    }

    public enum StateContact
    {
        Email,
        PhoneNum,
        FAX
    }

    public enum StateTransfer
    {
        HF,
        Unit
    }

    public enum StateProductGroup
    {
        Essential,
        Complementary
    }


    public enum StateInventory
    {
        IsProccess,
        NotComblete,
        IsCombleted
    }

    public enum ProductCategory
    {
        Nothing,
        NumberOperationEpiredDate,
        NumberOperation
    }
    public enum SupplierType
    {
        Sweetened = 1,
        Foreigner = 2,
        Factory = 3
    }
}
