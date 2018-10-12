using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UpdateAgentShare : DriverTestCase
    {
        void CreateNewAgent(UpdateAgentHelper helper, string code, string firstName, string lastName, string email, string Pasname)
        {
            if (string.Compare(code, "SA") == 0)
            {
                VisitOffice("/sales_agents");
                helper.WaitForText("Advanced Filter", 10000);
                helper.WaitForWorkAround(5000);
                helper.TypeText("AgentName", firstName + " " + lastName);
                helper.WaitForWorkAround(5000);
                if (helper.tableRow() <= 1)
                {
                    helper.ClickElement("Create");
                    create(helper, "FirstName", firstName, "LastName", lastName, "eAddressType", "eAddressLabel", "eAddress", email, "Save");
                }

            }
            else if (string.Compare(code, "PA") == 0)
            {
                VisitOffice("/partners/agents");
                helper.WaitForText("Advanced Filter", 10000);
                helper.WaitForWorkAround(5000);
                helper.TypeText("AgentName", firstName + " " + lastName);
                helper.WaitForWorkAround(5000);
                if (helper.tableRow() <= 1)
                {
                    helper.ClickElement("Create");

                    create(helper, "PAFirstName", firstName, "PALastName", lastName, "PAeAddressType", "PAeAddressLabel", "PAeAddress", email, "PASave");
                }
            }
            else if (string.Compare(code, "PAS") == 0)
            {
                VisitOffice("/partners/associations");
                helper.WaitForText("Advanced Filter", 10000);
                helper.WaitForWorkAround(5000);
                helper.TypeText("AgentName", Pasname);
                helper.TypeText("AgentNameLast", lastName);
                helper.TypeText("AgentNameEmail", email);
                helper.WaitForWorkAround(5000);
                if (helper.tableRow() <= 1)
                {
                    helper.ClickElement("Create");
                    helper.TypeText("AssocName", Pasname);
                    create(helper, "AssocFirstName", firstName, "AssocLastName", lastName, "AssoceAddressType", "AssoceAddressLabel", "AssoceAddress", email, "AssocSave");
                }
            }



        }
        void create(UpdateAgentHelper helper, string firstName, string fnInput, string lastName, string lnInput, string eType, string eLabel, string eAddress, string eAInput, string save)
        {

            helper.WaitForWorkAround(3000);
            helper.TypeText(firstName, fnInput);
            helper.TypeText(lastName, lnInput);
            helper.Select(eType, "E-Mail");
            helper.Select(eLabel, "Work");
            helper.TypeText(eAddress, eAInput);
            helper.ClickElement(save);
        }
        void updateShareHelper(UpdateAgentHelper helper, string code, string agent, string share)
        {
            if (helper.checkPresent(code + "Click"))
            {
                helper.doubleClickElement(code + "Click");
            }
            else
            {
                helper.doubleClickElement(code + "Edit");
            }


            helper.SelectByText(code + "AgentDrop", agent);


            helper.ClickElement(code + "SaveAgent");
            helper.doubleClickElement(code + "ShareClick");
            helper.ClearText(code + "ShareInput");
            helper.TypeText(code + "ShareInput", share);
            helper.ClickElement(code + "SaveShare");

            //helper.ClickElement("TeamShare");
            //helper.SelectDropDownByText("//*[@id='teamidvalues']", "Billy-Paul");
            //helper.TypeText("MemberShare", "30");
            //helper.Click("//*[@id='SalesCodes']/div[3]/a/button");


        }
        void UpdateShare(UpdateAgentHelper helper, string MID, string shareLine, ExecutionLog log)
        {
            helper.WaitForWorkAround(1000);
            VisitOffice("/clients");
            helper.WaitForWorkAround(3000); //made this longer to prevent error
            //helper.SelectByText("Responsibility", "All");
            helper.TypeText("MID", MID);
            helper.waitID(MID);
            try
            {
                helper.ClickID(MID);
                //helper.doubleClickElement("SAClick");
                //helper.SelectDropDownByText("//*[@id='manager']/form/select", "Billy Parra");
                //helper.Click("//*[@id='manager']/form/button[1]");

            }
            catch (Exception)
            {
                helper.WaitForWorkAround(2000);
                helper.ClickID(MID);
            }
            helper.ClickElement("ResidualIncome");

            var values = shareLine.Split(',');
            for (int i = 1; i < values.Length - 2; i = i + 3)
            {
                log.Log("UpdateAgent", values[i]);
                //var shares = values[i].Split('-');
                if (values[i].Length != 0)
                {
                    updateShareHelper(helper, values[i].Trim(), values[i + 1].Trim(), values[i + 2].Trim());
                }
            }
        }
        [TestMethod]
        public void ASLAMAGENTSHARE()
        {
            // put username and password here
            // change the office url in Config-ApplicationSettings.xml to be the url of user office site
            string username1 = "erica.garcia";
            string password1 = "eR1C@231";



            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var updateAgentHelper = new UpdateAgentHelper(GetWebDriver());
            executionLog.Log("UpdateAgent", "Login with valid username and password");
            Login(username1, password1);
            updateAgentHelper.WaitForWorkAround(2000);
            executionLog.Log("UpdateAgent", "Create agent");


            executionLog.Log("UpdateAgent", "Update share");
            // this is for updating share
            //put share.csv path here
            var readerMer = new StreamReader(File.OpenRead(@"C:\Users\India\Downloads\Final Rev. Share Import.csv"));
            var lineMer = readerMer.ReadLine();

            while (!readerMer.EndOfStream)
            {
                lineMer = readerMer.ReadLine().Trim();
                executionLog.Log("UpdateAgent", lineMer);
                if (lineMer.Length == 0)
                {
                    continue;
                }
                //lineMer = lineMer.Substring(1, lineMer.Length - 2);
                var values = lineMer.Split(',');
                UpdateShare(updateAgentHelper, values[0].Trim(), lineMer, executionLog);
            }

        }
    }
}
