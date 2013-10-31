using System;

namespace FastCgiNet
{
	internal class RecordFactory
	{
		public RecordBase CreateRecordFromHeader(byte[] header, int offset, int length, out int endOfRecord)
		{
			if (offset + 8 > header.Length)
				throw new InvalidOperationException("There are not enough bytes in the array for a complete header. Make sure at least 8 bytes are passed");
			if (ByteCopyUtils.CheckArrayBounds(header, offset, length) == false)
				throw new InvalidOperationException("Array bounds are wrong");

			var recordType = (RecordType)header[offset + 1];

			if (recordType == RecordType.FCGIBeginRequest)
			{
				return new BeginRequestRecord(header, offset, length, out endOfRecord);
			}
			else if (recordType == RecordType.FCGIEndRequest)
			{
				return new EndRequestRecord(header, offset, length, out endOfRecord);
			}
			else if (recordType == RecordType.FCGIStdin)
			{
				return new StdinRecord(header, offset, length, out endOfRecord);
			}
			else if (recordType == RecordType.FCGIParams)
			{
				return new ParamsRecord(header, offset, length, out endOfRecord);
			}
			else
			{
				throw new NotImplementedException("");
				//TODO: Other types of records
			}
		}
	}
}
