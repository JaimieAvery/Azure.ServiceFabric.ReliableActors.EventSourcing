namespace EventStore.DocumentDb
{
    using System;
    using System.Runtime.Serialization;

    public class TypeNameSerialisationBinder : SerializationBinder
    {
        public string TypeFormat { get; }

        public TypeNameSerialisationBinder(string typeFormat)
        {
            TypeFormat = typeFormat;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            string resolvedTypeName = string.Format(TypeFormat, typeName);

            return Type.GetType(resolvedTypeName, true);
        }
    }
}
