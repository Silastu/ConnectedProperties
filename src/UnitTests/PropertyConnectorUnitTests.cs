﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.ConnectedProperties;

namespace UnitTests
{
    [TestClass]
    public class PropertyConnectorUnitTests
    {
        [TestMethod]
        public void TryGet_ForValueTypeCarrier_ReturnsNull()
        {
            var carrier = 13;
            var name = Guid.NewGuid().ToString("N");
            Assert.IsNull(ConnectedPropertyScope.Default.TryGetConnectedProperty(carrier, name));
        }

        [TestMethod]
        public void TryGet_ForStringCarrier_ReturnsNull()
        {
            var carrier = "Hi";
            var name = Guid.NewGuid().ToString("N");
            Assert.IsNull(ConnectedPropertyScope.Default.TryGetConnectedProperty(carrier, name));
        }

        [TestMethod]
        public void TryGet_ForValueTypeCarrierWithoutValidation_DoesNotReturnNull()
        {
            // Please note: this is an extremely dangerous example! Do not use in real-world code!
            var carrier = 13;
            var name = Guid.NewGuid().ToString("N");
            Assert.IsNotNull(ConnectedPropertyScope.Default.TryGetConnectedProperty(carrier, name, bypassValidation: true));
        }

        [TestMethod]
        public void TryGet_ForStringCarrierWithoutValidation_DoesNotReturnNull()
        {
            // Please note: this is an extremely dangerous example! Do not use in real-world code!
            var carrier = "Hi";
            var name = Guid.NewGuid().ToString("N");
            Assert.IsNotNull(ConnectedPropertyScope.Default.TryGetConnectedProperty(carrier, name, bypassValidation: true));
        }

        [TestMethod]
        public void Get_ForValueTypeCarrier_Throws()
        {
            var carrier = 13;
            var name = Guid.NewGuid().ToString("N");
            var connector = ConnectedPropertyScope.Default;
            AssertEx.Throws<InvalidOperationException>(() => connector.GetConnectedProperty(carrier, name));
        }

        [TestMethod]
        public void Get_ForStringCarrier_Throws()
        {
            var carrier = "Hi";
            var name = Guid.NewGuid().ToString("N");
            var connector = ConnectedPropertyScope.Default;
            AssertEx.Throws<InvalidOperationException>(() => connector.GetConnectedProperty(carrier, name));
        }

        [TestMethod]
        public void Get_ForValueTypeCarrierWithoutValidation_DoesNotThrow()
        {
            // Please note: this is an extremely dangerous example! Do not use in real-world code!
            var carrier = 13;
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier, name, bypassValidation: true);
        }

        [TestMethod]
        public void Get_ForStringCarrierWithoutValidation_DoesNotThrow()
        {
            // Please note: this is a highly dangerous example! Do not use in real-world code unless you know for-sure what you're doing!
            var carrier = "Hi";
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier, name, bypassValidation: true);
        }

        [TestMethod]
        public void CopyAll_ForValueTypeSourceCarrier_Throws()
        {
            var carrier1 = 13;
            var carrier2 = new object();
            var connector = ConnectedPropertyScope.Default;
            AssertEx.Throws<InvalidOperationException>(() => connector.CopyAll(carrier1, carrier2));
        }

        [TestMethod]
        public void CopyAll_ForValueTypeTargetCarrier_Throws()
        {
            var carrier1 = new object();
            var carrier2 = 13;
            var connector = ConnectedPropertyScope.Default;
            AssertEx.Throws<InvalidOperationException>(() => connector.CopyAll(carrier1, carrier2));
        }

        [TestMethod]
        public void CopyAll_ForValueTypeCarriersWithoutValidation_DoesNotThrow()
        {
            var carrier1 = 13;
            var carrier2 = 17;
            ConnectedPropertyScope.Default.CopyAll(carrier1, carrier2, true);
        }

        [TestMethod]
        public void CopyAll_CopiesAllValues()
        {
            var carrier1 = new object();
            var carrier2 = new object();
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier1, name).Set(5);
            ConnectedPropertyScope.Default.CopyAll(carrier1, carrier2);
            Assert.AreEqual(5, ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Get());
        }

        [TestMethod]
        public void CopyAll_WhenTargetAlreadyHasValue_OverwritesTargetValue()
        {
            var carrier1 = new object();
            var carrier2 = new object();
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier1, name).Set(5);
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Set(7);
            ConnectedPropertyScope.Default.CopyAll(carrier1, carrier2);
            Assert.AreEqual(5, ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Get());
        }

        [TestMethod]
        public void TryCopyAll_ForValueTypeSourceCarrier_ReturnsFalse()
        {
            var carrier1 = 13;
            var carrier2 = new object();
            var result = ConnectedPropertyScope.Default.TryCopyAll(carrier1, carrier2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryCopyAll_ForValueTypeTargetCarrier_ReturnsFalse()
        {
            var carrier1 = new object();
            var carrier2 = 13;
            var result = ConnectedPropertyScope.Default.TryCopyAll(carrier1, carrier2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TryCopyAll_ForValueTypeCarriersWithoutValidation_ReturnsTrue()
        {
            var carrier1 = 13;
            var carrier2 = 17;
            var result = ConnectedPropertyScope.Default.TryCopyAll(carrier1, carrier2, true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryCopyAll_CopiesAllValues()
        {
            var carrier1 = new object();
            var carrier2 = new object();
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier1, name).Set(5);
            ConnectedPropertyScope.Default.TryCopyAll(carrier1, carrier2);
            Assert.AreEqual(5, ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Get());
        }

        [TestMethod]
        public void TryCopyAll_WhenTargetAlreadyHasValue_OverwritesTargetValue()
        {
            var carrier1 = new object();
            var carrier2 = new object();
            var name = Guid.NewGuid().ToString("N");
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier1, name).Set(5);
            ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Set(7);
            ConnectedPropertyScope.Default.TryCopyAll(carrier1, carrier2);
            Assert.AreEqual(5, ConnectedPropertyScope.Default.GetConnectedProperty(carrier2, name).Get());
        }
    }
}
