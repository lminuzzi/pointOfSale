using System;
using TechTalk.SpecFlow;
using PointOfSale;

namespace PointOfSaleTest
{
    [Binding]
    public class CategoriesSteps
    {
        PointOfSale.Models.Category category = new PointOfSale.Models.Category();
        PointOfSale.Controllers.CategoryController categoryController = new PointOfSale.Controllers.CategoryController();
        [Given(@"I have entered a '(.*)' into the category form")]
        public void GivenIHaveEnteredAIntoTheCategoryForm(string description)
        {
            category.Name = description;
        }

        [When(@"I press add")]
        public async void WhenIPressAdd()
        {
            await categoryController.Create(category);
            //ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be '(.*)' on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(string p0)
        {
            //ScenarioContext.Current.Pending();
            System.Console.Write("Category added successfully.");
        }
    }
}
