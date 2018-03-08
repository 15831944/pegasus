using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyValidationNotPresent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void verifyValidationNotPresent()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            //Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyValidationNotPresent", "Login with valid username and password");
                Login("testpartnerassociation", "test1qaz!QAZ");
            
                executionLog.Log("VerifyValidationNotPresent", "Verify Page title");
                VerifyTitle("test association - Details");

                executionLog.Log("VerifyValidationNotPresent", "Click on edit button.");
                agent_PartnerAssociationHelper.ClickElement("EditAsso");

                executionLog.Log("VerifyValidationNotPresent", "Enter a valid 10 digit phone number.");
                agent_PartnerAssociationHelper.TypeText("PhoneNum", "1111112222");

                executionLog.Log("VerifyValidationNotPresent", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("Save");

                executionLog.Log("VerifyValidationNotPresent", "Verify success message for details updation");
                agent_PartnerAssociationHelper.WaitForText("Partner details has been updated.", 10);

                executionLog.Log("VerifyValidationNotPresent", "Verify error message not present.");
                agent_PartnerAssociationHelper.VerifyTextNot("Please enter no more than 10 characters.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyValidationNotPresent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Validation Not Present");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Validation Not Present", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Validation Not Present");
                        TakeScreenshot("VerifyValidationNotPresent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidationNotPresent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyValidationNotPresent");
                        string id = loginHelper.getIssueID("Verify Validation Not Present");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyValidationNotPresent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Validation Not Present"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Validation Not Present");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyValidationNotPresent");
                executionLog.WriteInExcel("Verify Validation Not Present", Status, JIRA, "Agents Portal");
            }
        }
    }
}