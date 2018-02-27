namespace LostTech.Stack.Extensibility
{
    using System;
    using LostTech.Stack.Extensibility.Filters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SkypeTest
    {
        [TestMethod, Ignore]
        public void SkypeMustMatch() {
            var skype = new WindowFilter {
                ProcessFilter =
                    new CommonStringMatchFilter {
                        Match = CommonStringMatchFilter.MatchOption.Exact,
                        Value = "SkypeApp"
                    },
            };
            Assert.IsTrue(skype.Matches((IntPtr)0x000a0642));
        }
    }
}
