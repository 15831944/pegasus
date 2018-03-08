using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTypoInZipCodeAutoCityPopulate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyTypoInZipCodeAutoCityPopulate()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());
            var agent_PartnerAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var agents_PartnerAssociationHelper = new Agents_PartnerAssociationHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                //Verify typo on create employee page

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Redirect at Create employee page.");
                VisitOffice("employees/create");
                agents_EmployeesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Enter zip code");
                agents_EmployeesHelper.TypeText("PostalCode", "20001");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_EmployeesHelper.VerifyFieldValue("City", "Washington");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_EmployeesHelper.selectedOption("State", "DC");

                //Verify typo on create partner agent page

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Redirect at Create partner agent page.");
                VisitOffice("partners/agent/create");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Enter zip code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "20001");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agent_PartnerAgentHelper.VerifyFieldValue("City", "Washington");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agent_PartnerAgentHelper.selectedOption("State", "DC");

                //Verify typo on edit partner agent page

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Redirect at All partner agent page.");
                VisitOffice("partners/agents");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Edit a partner agent");
                agent_PartnerAgentHelper.ClickElement("FirstEditIcon");
                agent_PartnerAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Enter zip code");
                agent_PartnerAgentHelper.TypeText("PostalCode", "20001");
                agent_PartnerAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agent_PartnerAgentHelper.VerifyFieldValue("City", "Washington");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agent_PartnerAgentHelper.selectedOption("State", "DC");

                //Verify typo on create partner association page

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Redirect at Create partner agent page.");
                VisitOffice("partners/association/create");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Enter zip code");
                agents_PartnerAssociationHelper.TypeText("PostalCode", "20001");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_PartnerAssociationHelper.VerifyFieldValue("City", "Washington");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_PartnerAssociationHelper.selectedOption("State", "DC");

                //Verify typo on edit partner association page

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Redirect at All partner agent page.");
                VisitOffice("partners/associations");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Edit a partner agent");
                agents_PartnerAssociationHelper.ClickElement("EditAssociation1");
                agents_PartnerAssociationHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Enter zip code");
                agents_PartnerAssociationHelper.TypeText("PostalCode", "20001");
                agents_PartnerAssociationHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_PartnerAssociationHelper.VerifyFieldValue("City", "Washington");

                executionLog.Log("VerifyTypoInZipCodeAutoCityPopulate", "Verify City populated");
                agents_PartnerAssociationHelper.selectedOption("State", "DC");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTypoInZipCodeAutoCityPopulate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Typo In Zip Code Auto City Populate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Typo In Zip Code Auto City Populate", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Typo In Zip Code Auto City Populate");
                        TakeScreenshot("VerifyTypoInZipCodeAutoCityPopulate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTypoInZipCodeAutoCityPopulate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTypoInZipCodeAutoCityPopulate");
                        string id = loginHelper.getIssueID("Verify Typo In Zip Code Auto City Populate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTypoInZipCodeAutoCityPopulate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Typo In Zip Code Auto City Populate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Typo In Zip Code Auto City Populate");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTypoInZipCodeAutoCityPopulate");
                executionLog.WriteInExcel("Verify Typo In Zip Code Auto City Populate", Status, JIRA, "Agents Portal");
            }
        }
    }
}

