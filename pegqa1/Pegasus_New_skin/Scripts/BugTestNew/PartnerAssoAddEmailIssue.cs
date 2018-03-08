using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PartnerAssoAddEmailIssue : DriverTestCase
    {
        [TestMethod]
   //     [TestCategory("All")]
 //       [TestCategory("Bug")]
        [TestCategory("Temp")]
        public void partnerAssoAddEmailIssue()
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

            //      try
            //      {
            executionLog.Log("PartnerAssoAddEmailIssue", "Login with valid username and password");
            Login("testpartnerassociation", "test1qaz!QAZ");

            executionLog.Log("PartnerAssoAddEmailIssue", "Verify Page title");
            VerifyTitle("test association - Details");

            executionLog.Log("PartnerAssoAddEmailIssue", "Click on edit button.");
            agent_PartnerAssociationHelper.ClickElement("EditAsso");
            agent_PartnerAssociationHelper.WaitForWorkAround(2000);

            agent_PartnerAssociationHelper.WaitForElementPresent("RemoveEmail", 20);

            agent_PartnerAssociationHelper.ClickElement("RemoveEmail");
        
                executionLog.Log("PartnerAssoAddEmailIssue", "Click on add email..");
                agent_PartnerAssociationHelper.ClickElement("AddEmail");

                executionLog.Log("PartnerAssoAddEmailIssue", "Select eAddress type.");
                agent_PartnerAssociationHelper.Select("EmailType2", "E-Mail");

                executionLog.Log("PartnerAssoAddEmailIssue", "Enter added email.");
                agent_PartnerAssociationHelper.TypeText("EmailAdd2", "test@gmail.com");

                executionLog.Log("PartnerAssoAddEmailIssue", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("Save");

                executionLog.Log("PartnerAssoAddEmailIssue", "Verify added email present on page.");
                agent_PartnerAssociationHelper.VerifyeLabel("VerifyEmail");
                agent_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("PartnerAssoAddEmailIssue", "Click on edit button.");
                agent_PartnerAssociationHelper.ClickElement("EditAsso");

                executionLog.Log("PartnerAssoAddEmailIssue", "Click on remove icon.");
                agent_PartnerAssociationHelper.ClickElement("RemoveEmail");

                executionLog.Log("PartnerAssoAddEmailIssue", "Click on save button.");
                agent_PartnerAssociationHelper.ClickElement("Save");
            
        }
    } }
       /*  catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PartnerAssoAddEmailIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Partner Asso Add Email Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Partner Asso Add Email Issue", "Bug", "Medium", "Partner Association page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Partner Asso Add Email Issue");
                        TakeScreenshot("PartnerAssoAddEmailIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoAddEmailIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PartnerAssoAddEmailIssue");
                        string id = loginHelper.getIssueID("Partner Asso Add Email Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PartnerAssoAddEmailIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Partner Asso Add Email Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Partner Asso Add Email Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PartnerAssoAddEmailIssue");
                executionLog.WriteInExcel("Partner Asso Add Email Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}*/