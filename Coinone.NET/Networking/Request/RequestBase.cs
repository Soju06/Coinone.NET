﻿using Soju06.Web.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request
{
    /// <summary>
    /// 자동 직렬화 가능한 요청 베이스
    /// </summary>
    [Obsolete]
    [DataContract]
    public abstract class CoinoneRequestBase {
        /// <summary>
        /// Unix 시간
        /// </summary>
        [DataMember(Name = "nonce")]
        public long Nonce { get; set; }
    }

    /// <summary>
    /// 수동 직렬화 가능한 요청 베이스
    /// </summary>
    public abstract class CoinoneSerializableRequestBase {
        /// <summary>
        /// Unix 시간
        /// </summary>
        public long Nonce { get => ((DateTimeOffset)CoinoneNetwork.CoinoneNowDateTime)
                .ToUnixTimeSeconds() + NonceTimeLimit; }

        /// <summary>
        /// 제한시간
        /// </summary>
        public long NonceTimeLimit { get; set; } = 2000;

        internal XDocument Serialize() {
            var e = JsonUtility.CreateDocument();
            var r = e.Root;
            r.Add(JsonUtility.CreateElement("nonce", Nonce, "number"));
            SerializeObject(ref r);
            return e;
        }

        /// <summary>
        /// 직렬화
        /// </summary>
        /// <param name="element">대상 개체</param>
        protected abstract void SerializeObject(ref XElement element);
    }
}
