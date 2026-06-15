using System;
using System.Collections.Generic;
using System.Text;

namespace BoscoAFH.Common.Models
{
    public class ListArrayResult
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dropdown item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display value for the dropdown item.
        /// </summary>
        public string? Value { get; set; }

        public string? ItemName { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDefault { get; set; } = false;

        public short? Type { get; set; } = 0;
        public decimal Charge { get; set; }

        public Guid? RelatedId { get; set; } = Guid.Empty;

    }
    public class ListArrayImageResult
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public string? ImageName { get; set; }
        public string? Path { get; set; }
        public string BaseBinary { get; set; } = string.Empty;
    }

    public class GenericDropdownObjects<X> where X : new()
    {
        public X Id { get; set; } = new();
        public string Value { get; set; } = null!;
    }

    /// <summary>
    /// Represents a generic model for dropdown fetches, including a unique identifier and display value.
    /// </summary>
    public class ListTaxArrayResult
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dropdown item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display value for the dropdown item.
        /// </summary>
        public string Value { get; set; } = null!;

        public decimal TaxPercentage { get; set; }

        public static implicit operator ListTaxArrayResult(List<ListTaxArrayResult> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ListArrayIntergerResult
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dropdown item.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the display value for the dropdown item.
        /// </summary>
        public string Value { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }

    public class ListArrayStringResult
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dropdown item.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the display value for the dropdown item.
        /// </summary>
        public string Value { get; set; } = null!;
    }

    public static class GuidAllResult
    {
        public static readonly Guid AllGuidValue = Guid.Parse("11111111-2222-3333-4444-555555555555");
        public static readonly int AllIntValue = -1;
        public static ListArrayResult GetGuidPair()
        {
            return new ListArrayResult { Id = AllGuidValue, Value = "All" };
        }
        public static ListArrayIntergerResult GetIntPair()
        {
            return new ListArrayIntergerResult { Id = AllIntValue, Value = "All" };
        }

        public static List<ListArrayResult> GetGuidPairWithResult(List<ListArrayResult> data)
        {
            if (data.Count > 0)
            {
                data.Insert(0, GetGuidPair());
            }

            return data;
        }

        public static List<ListArrayIntergerResult> GetIntPairWithResult(this List<ListArrayIntergerResult> data)
        {
            if (data.Count > 0)
            {
                data.Insert(0, GetIntPair());
            }

            return data;
        }
    }
}
