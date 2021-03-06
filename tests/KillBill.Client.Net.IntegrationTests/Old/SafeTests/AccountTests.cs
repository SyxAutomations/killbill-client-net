﻿using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KillBill.Client.Net.IntegrationTests.SafeTests
{
    [TestFixture]
    public class AccountTests : BaseTestFixture
    {
        [Test]
        [TestCase("lister-bundle-a053363e-933e-4d16-91ab-a65c41111bf8")]
        public async Task Get_Bundle(string bundleKey)
        {
            // act
            var bundle = await Client.GetBundle(bundleKey, RequestOptions);

            // assert 
            Assert.That(bundle, Is.Not.Null);
        }

        [Test]
        public async Task Get_Bundles()
        {
            // act
            var bundles = await Client.GetAccountBundles(AccountId, RequestOptions);

            // assert
            if (!bundles.Any())
                Assert.Inconclusive("No bundles found for account.");

            Console.WriteLine($"Found {bundles.Count} bundles for account");
            var firstBundle = bundles.First();
            Console.WriteLine($"Testing first bundle with key - {firstBundle.ExternalKey}");
            Assert.That(firstBundle.AccountId, Is.EqualTo(AccountId));
            Assert.That(firstBundle.ExternalKey, Is.Not.Null);
            Assert.That(firstBundle.ExternalKey, Is.Not.Empty);
        }

        [Test]
        public async Task Get_Invoices()
        {
            // act
            var invoices = await Client.GetInvoicesForAccount(AccountId, RequestOptions);

            // assert
            if (!invoices.Any())
                Assert.Inconclusive("No invoices found for account.");

            Console.WriteLine($"Found {invoices.Count} invoices for account {AccountId}");
            var invoice = invoices.First();
            Assert.That(invoice.AccountId, Is.EqualTo(AccountId));
            Assert.That(invoice.InvoiceId, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public async Task Get_Account_Timeline()
        {
            // act 
            var timeline = await Client.GetAccountTimeline(AccountId, RequestOptions);

            // assert
            Assert.That(timeline, Is.Not.Null);
            Assert.That(timeline.Account, Is.Not.Null);
            Assert.That(timeline.Account.AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public async Task Get_Emails_For_Account()
        {
            // act
            var emails = await Client.GetEmailsForAccount(AccountId, RequestOptions);

            // assert
            if (!emails.Any())
                Assert.Inconclusive("No emails found for account.");

            Assert.That(emails.First().AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public async Task Get_Payments_For_Account()
        {
            // act
            var payments = await Client.GetPaymentsForAccount(AccountId, RequestOptions);

            // assert
            if (!payments.Any())
                Assert.Inconclusive("No payments found for account.");

            Assert.That(payments.First().AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        public async Task Get_InvoicePayments_For_Account()
        {
            // act
            var invoicePayments = await Client.GetInvoicePaymentsForAccount(AccountId, RequestOptions);

            // assert
            if (!invoicePayments.Any())
                Assert.Inconclusive("No invoice payments found for account.");

            Assert.That(invoicePayments.First().AccountId, Is.EqualTo(AccountId));
        }
    }
}