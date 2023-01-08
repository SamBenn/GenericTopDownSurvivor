using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Constants
{
    public static class StatConstants
    {
        // public stats
        public static Guid Health { get; } = new Guid("1b69833d-7ce1-42a3-a1fa-0ddeb3f309de");
        public static Guid MovementSpeed { get; } = new Guid("8a2bedeb-0c9b-4b81-9821-9c46ef76092a");
        public static Guid Cooldown { get; } = new Guid("dd74c0e9-3a8e-42b1-8a32-4db09afe4a5c");
        public static Guid GenericDamage { get; } = new Guid("f309c5e5-8957-4dbc-a179-a8fae6b1232f");
        public static Guid FireDamage { get; } = new Guid("d0440e34-fc83-4047-bc99-c83a897f8d01");
        public static Guid IceDamage { get; } = new Guid("3adbb138-883a-489d-8e7b-156020c868fe");
        public static Guid LightningDamage { get; } = new Guid("623457cf-c9d1-40d0-bc7c-aae5b054458c");
        public static Guid BladeDamage { get; } = new Guid("1f589b47-9466-4127-b014-0f1c5a3cbf21");
        public static Guid AreaOfEffect { get; } = new Guid("0279788b-b682-411d-b96a-a343ec99da74");

        // passive stats
        public static Guid P_AdditionalProjectiles { get; } = new Guid("7f7f0756-0273-4af5-889b-01d78c89d5e4");
    }
}
