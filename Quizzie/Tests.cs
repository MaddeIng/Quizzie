using Xunit;
using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie
{
    public class Tests
    {
        [Fact]
        public void TestIfQuizExists()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            bool quizExists = context.Quizs
                .Any(q => q.AccessCode == 9999);

            Assert.True(quizExists);
        }
    }
}