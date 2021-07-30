﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace createQrcode.Models
{
    public class Qrcode
    {
        private string v_00;
        private string v_01;
        private string v_26_00;
        private string v_26_01;
        private string v_52;
        private string v_53;
        private string v_54;
        private string v_58;
        private string v_59;
        private string v_60;
        private string v_61;
        private string v_62_01;
        private string v_62_03;
        private string v_62_07;
        private string v_62_08;
        private string v_62_09;
        public Qrcode(string v_00,
              string v_01,
              string v_26_00,
              string v_26_01,
              string v_52,
              string v_53,
              string v_54,
              string v_58,
              string v_59,
              string v_60,
              string v_61,
              string v_62_01,
              string v_62_03,
              string v_62_07,
              string v_62_08,
              string v_62_09
              )
        {
            this.v_00 = v_00;
            this.v_01 = v_01;
            this.v_26_00 = v_26_00;
            this.v_26_01 = v_26_01;
            this.v_52 = v_52;
            this.v_53 = v_53;
            this.v_54 = v_54;
            this.v_58 = v_58;
            this.v_59 = v_59;
            this.v_60 = v_60;
            this.v_61 = v_61;
            this.v_62_01 = v_62_01;
            this.v_62_03 = v_62_03;
            this.v_62_07 = v_62_07;
            this.v_62_08 = v_62_08;
            this.v_62_09 = v_62_09;

        }
        public Qrcode(
            string merchantId,
            string merchantGuid,
            string merchantName,
            string merchantCateloryCode,
            string storeLable,
            string terminalLable,
            string countryCode,
            string merchantCity,
            string ccy,
            string postalCode,
            string billNumber,
            string amount
            
            )
        {
            this.v_00 = "00";
            this.v_01 = "12";
            this.v_26_00 = merchantGuid;
            this.v_26_01 = merchantId;
            this.v_52 = merchantCateloryCode;
            this.v_53 = ccy;
            this.v_54 = amount;
            this.v_58 = countryCode;
            this.v_59 = merchantName;
            this.v_60 = merchantCity;
            this.v_61 = postalCode;
            this.v_62_01 = billNumber;
            this.v_62_03 = storeLable;
            this.v_62_07 = terminalLable;
            this.v_62_08 = "";
            this.v_62_09 = "ME";
        }
        public Qrcode(ReqQrcode data)
        {
            this.v_00 = "01";
            this.v_01 = "12";
            this.v_26_00 = data.merchantGuid;
            this.v_26_01 = data.merchantId;
            this.v_52 = data.merchantCateloryCode;
            this.v_53 = data.ccy;
            this.v_54 = data.amount;
            this.v_58 = data.countryCode;
            this.v_59 = data.merchantName;
            this.v_60 = data.merchantCity;
            this.v_61 = data.postalCode;
            this.v_62_01 = data.billNumber;
            this.v_62_03 = data.storeLable;
            this.v_62_07 = data.terminalLable;
            this.v_62_08 = "";
            this.v_62_09 = "ME";
        }

        private string calc_crc(string val)
        {
            var crc = 0xFFFF;
            var polynomial = 0x1021; // 0001 0000 0010 0001  (0, 5, 12)
            var bytes = Encoding.ASCII.GetBytes(val);
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    var bit = ((bytes[i] >> (7 - ii) & 1) == 1);
                    var c15 = ((crc >> 15 & 1) == 1);
                    crc <<= 1;
                    if (c15 ^ bit)
                    {
                        crc ^= polynomial;
                    }
                }
            }
            crc &= 0xffff;
            var kq = crc.ToString("X");
           return kq.PadLeft(4, '0');
            //return crc.ToString();

        }
        private string calc_val(string key, string val)
        {
            var lval = val.Length.ToString().PadLeft(2, '0');
            var kq = key + lval + val;
            return kq;
        }
        private string f_00() { return this.calc_val("00", this.v_00); }
        private string f_01() { return this.calc_val("01", this.v_01); }
        private string f_26() {
            var v_00 = this.calc_val("00", this.v_26_00);
            var v_01 = this.calc_val("01", this.v_26_01);
            return this.calc_val("26", v_00 + v_01);
        }
       
        private string f_52() { return this.calc_val("52", this.v_52); }
        private string f_53() { return this.calc_val("53", this.v_53); }
        private string f_54() { return this.calc_val("54", this.v_54); }
        private string f_58() { return this.calc_val("58", this.v_58); }
        private string f_59() { return this.calc_val("59", this.v_59); }
        private string f_60() { return this.calc_val("60", this.v_60); }
        private string f_61() { return this.calc_val("61", this.v_61); }
        private string f_62() {
            var v_01 = this.calc_val("01", this.v_62_01);
            var v_03 = this.calc_val("03", this.v_62_03);
            var v_07 = this.calc_val("07", this.v_62_07);
            var v_08 = this.calc_val("08", this.v_62_08);
            var v_09 = this.calc_val("09", this.v_62_09);
            return this.calc_val("62", v_01 + v_03 + v_07 + v_08 + v_09);
        }
       
        private string f_63() {
            return "6304" + this.calc_crc(
                this.f_00() + this.f_01() + this.f_26() + this.f_52() +
                this.f_53() + this.f_54() + this.f_58() + this.f_59() +
                this.f_60() + this.f_61() + this.f_62() + "6304"
                );
        }
        public string create()
        {
            return this.f_00() + this.f_01() + this.f_26() + this.f_52() +
                this.f_53() + this.f_54() + this.f_58() + this.f_59() +
                this.f_60() + this.f_61() + this.f_62() + this.f_63();
        }

      


    }
}
