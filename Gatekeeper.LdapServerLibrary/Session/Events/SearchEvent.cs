using System.Collections.Generic;

namespace Gatekeeper.LdapServerLibrary.Session.Events
{
    public class SearchEvent
    {
        public string BaseObject { get; set; } = null!;
        public ScopeEnum Scope { get; set; }
        public DerefAliasesEnum DerefAliases { get; set; }
        public int? SizeLimit { get; set; }
        public int TimeLimit { get; set; }
        public bool TypesOnly { get; set; }
        public IFilterChoice Filter { get; set; } = null!;
        public List<string> AttributeSelection { get; set; } = new List<string>();

        public enum ScopeEnum
        {
            BaseObject = 1,
            SingleLevel = 2,
            WholeSubtree = 3,
        }

        public enum DerefAliasesEnum
        {
            NeverDerefAliases = 0,
            DerefInSearching = 1,
            DerefFindingBaseObj = 2,
            DerefAlways = 3,
        }

        public interface IFilterChoice { }
        public abstract class NestedFilterChoice : IFilterChoice
        {
            public List<IFilterChoice> Filters { get; set; } = new List<IFilterChoice>();
        }
        public class AndFilter : NestedFilterChoice { }
        public class OrFilter : NestedFilterChoice { }
        public class NotFilter : IFilterChoice
        {
            public IFilterChoice Filter { get; set; } = null!;
        }
        public abstract class AttributeValueAssertionFilter : IFilterChoice
        {
            public string AttributeDesc = null!;
            public string AssertionValue = null!;
        }
        public class EqualityMatchFilter : AttributeValueAssertionFilter { }
        public class SubstringFilter : IFilterChoice
        {
            public string? Type;
            public string? Initial;
            public List<string>? Any;
            public string? Final;
        }
        public class GreaterOrEqualFilter : AttributeValueAssertionFilter { }
        public class LessOrEqualFilter : AttributeValueAssertionFilter { }
        public class PresentFilter : IFilterChoice
        {
            public string Value = null!;
        }
        public class ApproxMatchFilter : AttributeValueAssertionFilter { }
        public class MatchingRuleAssertionFilter : IFilterChoice
        {
            public string? MatchingRule { get; set; }
            public string? Type { get; set; }
            public string MatchValue { get; set; } = null!;
            public bool DnAttributes;
        }
    }
}