﻿using System;
using System.Collections.Generic;
using KillBill.Client.Net.Model;
using NUnit.Framework;

namespace KillBill.Client.Net.Tests.ModificationTests
{
    [TestFixture]
    public class SubscriptionModificationTests : BaseTestFixture
    {
        private readonly string _externalKey = "aaaaa";

        [Test]
        public void Create_New_Subscriptions_WithAddons()
        {
            // arrange
            var bundleKey = Guid.NewGuid();
            var subscriptions = new List<Subscription>
            {
                new Subscription
                {
                  AccountId = AccountId,
                  PlanName = "system-connect-monthly",
                  ProductName = "system-connect",
                  ProductCategory = "BASE",
                  BillingPeriod = "MONTHLY",
                  ExternalKey = $"system-connect-" + bundleKey,
                },

                new Subscription
                {
                    AccountId = AccountId,
                    PlanName = "external-site-monthly",
                    ProductName = "external-site",
                    ProductCategory = "ADD_ON",
                    BillingPeriod = "MONTHLY",
                    ExternalKey = $"system-connect-" + bundleKey,
                }
            };

            // act
            var bundle = Client.CreateSubscriptionsWithAddOns(subscriptions, RequestOptions);

            // assert
            Assert.That(bundle, Is.Not.Null);
        }

        [Test]
        public void Create_New_Subscription_Bundle()
        {
            // arrange
            var subscription = new Subscription
            {
                AccountId = AccountId,
                ExternalKey = _externalKey,
                ProductName = "Standard",
                ProductCategory = "BASE",
                BillingPeriod = "MONTHLY",
                PriceList = "DEFAULT",
                StartDate = DateTime.Now
            };

            // act
            var bundle = Client.CreateSubscription(subscription, RequestOptions);

            // assert
            Assert.That(bundle, Is.Not.Null);
            Assert.That(bundle.AccountId, Is.EqualTo(AccountId));
        }

        [Test]
        [Ignore("I THINK i've misunderstood bundles<->subscriptions. 9:30am after an all nighter... time to sleep and resume tomorrow... or the day after.")]
        public void Add_Subscription_To_Bundle()
        {
            // arrange
            var existingBundle = Client.GetBundle(_externalKey, RequestOptions);
            Assume.That(existingBundle, Is.Not.Null, "We can't add a second subscription if the bundle doesnt exist... run the above test first.");

            var subscription = new Subscription
            {
                AccountId = AccountId,
                BundleId = existingBundle.BundleId,
                ExternalKey = _externalKey,
                ProductName = "Sports",
                ProductCategory = "BASE",
                BillingPeriod = "MONTHLY",
                PriceList = "DEFAULT",
                StartDate = DateTime.Now
            };

            // act
            var secondSubcription = Client.CreateSubscription(subscription, RequestOptions);

            // assert
            Assert.That(secondSubcription, Is.Not.Null);
            Assert.That(secondSubcription.BundleId, Is.EqualTo(existingBundle.BundleId));
        }
    }
}