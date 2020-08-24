using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MyStore.Models
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double PricePerUnit { get; set; }
        public string Unit { get; set; }
        public double? Discount { get; set; }
        public int? VolumeQuantity { get; set; }
        public double? VolumePrice { get; set; }

        public bool HasVolumeDeal => VolumeQuantity.HasValue && VolumePrice.HasValue;

        public double VolumeDiscount
        {
            get
            {
                if (HasVolumeDeal)
                    return VolumeQuantity.Value * PricePerUnit - VolumePrice.Value;

                return 0;
            }
        }

        public string VolumePackDescription
        {
            get
            {
                if (HasVolumeDeal)
                    return $"{VolumeQuantity.Value} for ${VolumePrice.Value}";

                return string.Empty;
            }
        }

        public string ProductDescription
        {
            get
            {
                if (Discount.HasValue)
                    return $"{ProductName} ${string.Format("{0:0.00}", PricePerUnit - Discount)}";
                else
                    return $"{ProductName} ${string.Format("{0:0.00}", PricePerUnit)}";
            }
        }

        public string VolumeDealDescription
        {
            get
            {
                if (HasVolumeDeal)
                    return $"Quantity: {VolumePackDescription}\tDiscount: ${string.Format("{0:0.00}", VolumeDiscount)}";
                else return string.Empty;
            }
        }

        public string DiscountDescription
        {
            get
            {
                if (Discount.HasValue)
                    return $"\n\t\t\tSaved: ${string.Format("{0:0.00}", Discount)}";
                else return string.Empty;
            }
        }

        public override string ToString()
        {
            var dealDescription = string.Empty;

            if (HasVolumeDeal)
                dealDescription = $" or {VolumeQuantity.Value} for ${VolumePrice.Value}";

            return $"{ProductCode} - {ProductName} @ ${string.Format("{0:0.00}", PricePerUnit)} {Unit}" + dealDescription;
        }
    }
}