using Shared.Data;
using System.Collections;
using System.Text;

namespace Shared.Dto.Requests;

public abstract class MultiEntityRequest : Request
{
    protected List<string> includes = [];
    protected Dictionary<string, OrderByDirection> orderBys = [];
    protected List<string> filters = [];
    protected bool allParenthesisClosed = true;
    protected int numberOfOpenParenthesis = 0;
    protected uint maximumNumberOfPageLinks = 9;

    public IEnumerable<string> Includes => includes;

    public string OrderBy => orderBys.Count > 0 ? string.Join(", ", orderBys.Select(o => $"{o.Key} {o.Value}")) : string.Empty;

    public string Filter => GetFilterString();

    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }

    public bool IsPagingEnabled => PageNumber.HasValue && PageSize.HasValue && MaximumNumberOfPageLinks > 0;

    public int? Skip => IsPagingEnabled ? (PageNumber!.Value - 1) * PageSize!.Value : null;

    public int? Take => IsPagingEnabled ? PageSize : null;

    public uint MaximumNumberOfPageLinks { get => maximumNumberOfPageLinks; set => maximumNumberOfPageLinks = (value % 2 == 0) ? value - 1 : value; }

    public int MedianPage
    {
        get
        {
            if (maximumNumberOfPageLinks == 0)
                return 0;

            return (int)Math.Ceiling(maximumNumberOfPageLinks / 2.0);
        }
    }

    #region Loading Related Data

    public void AddInclude(string include)
    {
        if (!includes.Contains(include))
        {
            includes.Add(include);
        }
    }

    public void RemoveInclude(string include)
    {
        if (includes.Contains(include))
        {
            includes.Remove(include);
        }
    }

    public void ClearIncludes()
    {
        includes.Clear();
    }

    #endregion

    #region Ordering Data

    public void AddOrderBy(string field, OrderByDirection direction)
    {
        orderBys.TryAdd(field, direction);
    }

    public void RemoveOrderBy(string field)
    {
        orderBys.Remove(field);
    }

    public void ClearOrderBys()
    {
        orderBys.Clear();
    }

    #endregion

    #region Filtering Data

    public void AddFilter<T>(string columnName, ComparisonOperator comparisonOperator, T value, LogicalOperator logicalOperator = LogicalOperator.None, Parenthesis parenthesis = Parenthesis.None)
    {
        var filterBuilder = new StringBuilder(filters.Any() ? " " : string.Empty);

        // Append logical operator if needed
        if (filters.Count > 0 && logicalOperator != LogicalOperator.None)
        {
            var logicalOp = logicalOperator switch
            {
                LogicalOperator.AND => SearchOperators.And,
                LogicalOperator.OR => SearchOperators.Or,
                LogicalOperator.NOT => SearchOperators.Not,
                _ => throw new NotSupportedException($"Logical operator '{logicalOperator}' is not supported.")
            };

            filterBuilder.Append($"{logicalOp} ");
        }

        // Append parenthesis if needed
        if (parenthesis == Parenthesis.Open)
        {
            filterBuilder.Append('(');
            allParenthesisClosed = false;
            numberOfOpenParenthesis++;
        }

        // Append column name and comparison operator
        var comparisonOp = comparisonOperator switch
        {
            ComparisonOperator.Equal => SearchOperators.Equal,
            ComparisonOperator.NotEqual => SearchOperators.NotEqual,
            ComparisonOperator.GreaterThan => SearchOperators.GreaterThan,
            ComparisonOperator.LessThan => SearchOperators.LessThan,
            ComparisonOperator.GreaterThanOrEqual => SearchOperators.GreaterThanOrEqual,
            ComparisonOperator.LessThanOrEqual => SearchOperators.LessThanOrEqual,
            ComparisonOperator.Like => SearchOperators.Like,
            ComparisonOperator.In => SearchOperators.In,
            ComparisonOperator.Between => SearchOperators.Between,
            ComparisonOperator.StarsWith => SearchOperators.Like,
            ComparisonOperator.EndsWith => SearchOperators.Like,
            _ => throw new NotSupportedException($"Comparison operator '{comparisonOperator}' is not supported.")
        };

        filterBuilder.Append($"{columnName} {comparisonOp} ");

        // Append value based on its type
        if (value is string stringValue)
        {
            if (comparisonOperator == ComparisonOperator.StarsWith)
            {
                stringValue = $"{stringValue}%";
            }
            else if (comparisonOperator == ComparisonOperator.EndsWith)
            {
                stringValue = $"%{stringValue}";
            }
            filterBuilder.Append($"'{stringValue}'");
        }
        else if (value is DateTime dateTimeValue)
        {
            filterBuilder.Append($"'{dateTimeValue:yyyy-MM-dd HH:mm:ss}'");
        }
        else if (value is bool boolValue)
        {
            filterBuilder.Append(boolValue ? "1" : "0");
        }
        else if (value is IEnumerable values)
        {
            if (comparisonOperator != ComparisonOperator.In && comparisonOperator != ComparisonOperator.Between)
            {
                throw new InvalidOperationException($"The comparison operator '{comparisonOperator}' is not valid for enumerable values.");
            }

            if (comparisonOp == SearchOperators.In)
            {
                var valueList = new List<string>();
                foreach (var item in values)
                {
                    if (item is string strItem)
                    {
                        valueList.Add($"'{strItem}'");
                    }
                    else if (item is DateTime dtItem)
                    {
                        valueList.Add($"'{dtItem:yyyy-MM-dd HH:mm:ss}'");
                    }
                    else if (item is bool bItem)
                    {
                        valueList.Add(bItem ? "1" : "0");
                    }
                    else
                    {
                        valueList.Add(item.ToString() ?? string.Empty);
                    }
                }
                filterBuilder.Append($"({string.Join(", ", valueList)})");
            }
            else if (comparisonOp == SearchOperators.Between)
            {
                var enumerator = values.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    var startValue = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        throw new InvalidOperationException("Between operator requires two values.");
                    }
                    var endValue = enumerator.Current;
                    filterBuilder.Append($"{startValue} AND {endValue}");
                }
            }
        }
        else
        {
            filterBuilder.Append(value);
        }

        // Append closing parenthesis if needed
        if (parenthesis == Parenthesis.Close)
        {
            if (!allParenthesisClosed && numberOfOpenParenthesis > 0)
            {
                filterBuilder.Append(')');
                numberOfOpenParenthesis--;
                if (numberOfOpenParenthesis == 0)
                {
                    allParenthesisClosed = true;
                }
            }
            else
            {
                throw new InvalidOperationException("Cannot close parenthesis that was not opened.");
            }
        }
    }

    protected string GetFilterString()
    {
        if (filters.Count != 0 && allParenthesisClosed)
        {
            return string.Join(" ", filters);
        }

        return string.Empty;
    }

    public void ClearFilters()
    {
        filters.Clear();
        allParenthesisClosed = true;
        numberOfOpenParenthesis = 0;
    }

    #endregion
}
