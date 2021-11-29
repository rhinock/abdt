using _01.Enums;
using System;

namespace _01.Filters
{
    /// <summary>
    /// User filter
    /// </summary>
    public class UserFilterRequest
    {
        /// <summary>
        /// User ids
        /// </summary>
        public long[] Ids { get; set; } = Array.Empty<long>();

        /// <summary>
        /// Users name like startWith
        /// </summary>
        public string? Name { get; set; }

        public Role[] Roles { get; set; } = Array.Empty<Role>();
    }
}
