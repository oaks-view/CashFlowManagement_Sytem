using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CashFlowManagement.Core.Services;
using CashFlowManagement.Tests.Core;
using CashFlowManagement.Web.Controllers;
using CashFlowManagement.Core.Models;
using static CashFlowManagement.Tests.Core.TestData;
using EmployeeManagement.Tests;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Linq;

namespace CashFlowManagement.Tests.web
{
    [TestClass]
    public class ExpenseControllerTests
    {
        private Mock<IExpenseService> _mockExpenseService;

        private Dictionary<string, int> _monthlyExpensesVal
            = new Dictionary<string, int> { { "4", 450000 }, { "5", 450000 } };

        private Dictionary<string, int> _yearlyExpensesVal
            = new Dictionary<string, int> { { "2015", 9000000 }, { "2016", 12000000 } };

        [TestInitialize]
        public void BeforeEachTest()
        {
            _mockExpenseService = new Mock<IExpenseService>();

            _mockExpenseService
                .Setup(x => x.GetAllExpense())
                .Returns(TestData._sampleExpenses);

            _mockExpenseService
                .Setup(x => x.GetExpense(It.IsAny<int>()))
                .Returns((int arg) =>
                {
                    return TestData._sampleExpenses.Find(x => x.Id == arg);
                });

            _mockExpenseService
                .Setup(x => x.SaveExpense(It.IsAny<Expense>())).Verifiable();

            _mockExpenseService
                .Setup(x => x.GetMonthlyExpenses())
                .Returns(_monthlyExpensesVal);
            _mockExpenseService
                .Setup(x => x.GetYearlyExpenses())
                .Returns(_yearlyExpensesVal);

            _mockExpenseService
                .Setup(x => x.DeleteExpense(It.IsAny<int>())).Verifiable();

            _mockExpenseService
                .Setup(x => x.GetStaffExpenses(It.IsAny<string>()))
                .Returns((string input) =>
                {
                    return _sampleExpenses.Where(e => e.StaffId == input).ToList();
                });
        }
        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_Can_Retrieve_All_Saved_Expenses()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            Expense savedExpense = _sampleExpenses[0];
            var apiCallResult = controller.Get();
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<List<Expense>>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Get_With_Parameter_Can_Retrieve_Saved_Expense()
        {
            Expense newExpense = _sampleExpenses[0];
            var controller = new ExpenseController(_mockExpenseService.Object);
            var apiCallResult = controller.Get(newExpense.Id);
            Assert.IsNotNull(apiCallResult);
            Assert.IsNotInstanceOfType(apiCallResult, typeof(OkResult));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Post_Can_Save_New_Expense()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            var newExpense = new Expense("AnyExpense", 80000, sampleEmployee.Id);
            controller.Post(newExpense);
            _mockExpenseService.Verify(x => x.SaveExpense(It.Is<Expense>(exp =>
            exp.Description == "AnyExpense"
            && exp.Cost == 80000
            )));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void Put_Can_Updated_Saved_Expense()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            Expense savedExpense = _sampleExpenses[0];
            savedExpense.Description = "New_Description";
            savedExpense.Cost = 11222;
            controller.Put(savedExpense);
            _mockExpenseService.Verify(x => x.SaveExpense(It.Is<Expense>(exp=>
            exp.Description == "New_Description"
            &&exp.Cost == 11222
            )));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetMonthlyExpenses_API_CALL_Works_Correctly()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            var apiCallResult = controller.GetMonthlyExpenses();
            Assert.IsInstanceOfType(apiCallResult,typeof(OkNegotiatedContentResult<Dictionary<string, int>>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetYearlyExpenses_API_Call_Works_Correctly()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            var apiCallResult = controller.GetYearlyExpenses();
            Assert.IsInstanceOfType(apiCallResult, typeof(OkNegotiatedContentResult<Dictionary<string, int>>));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void DELETE_Method_Works_Correctly()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            Expense savedExpense = _sampleExpenses[0];
            controller.Delete(savedExpense.Id);
            _mockExpenseService.Verify(x => x.DeleteExpense(It.Is<int>(input =>
            input == savedExpense.Id
            )));
        }

        [TestMethod, TestCategory(Constants.UnitTest)]
        public void GetStaffExpenses_Can_Retrieve_A_Staffs_Expenses()
        {
            var controller = new ExpenseController(_mockExpenseService.Object);
            var apiCallValue = controller.GetStaffExpenses(sampleEmployee.Id);
            Assert.IsInstanceOfType(apiCallValue, typeof(OkNegotiatedContentResult<List<Expense>>));
        }
    }
}
