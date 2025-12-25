namespace Shared.Data;

public static class SearchOperators
{
    #region Logical Operators

    public const string And = "AND";

    public const string Or = "OR";

    public const string Not = "NOT";

    public const string In = "IN";

    public const string Like = "LIKE";

    public const string Between = "BETWEEN";

    public const string Exists = "EXISTS";

    public const string All = "ALL";

    public const string Any = "ANY";

    public const string Some = "SOME";

    #endregion

    #region Comparison Operators

    public const string Equal = "==";

    public const string NotEqual = "!=";

    public const string GreaterThan = ">";  

    public const string LessThan = "<";

    public const string GreaterThanOrEqual = ">=";

    public const string LessThanOrEqual = "<=";

    #endregion
}
