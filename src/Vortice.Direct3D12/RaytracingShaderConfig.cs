﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// A state subobject that represents a shader configuration.
    /// </summary>
    public partial struct RaytracingShaderConfig : IStateSubObjectDescription, IStateSubObjectDescriptionMarshal
    {
        StateSubObjectType IStateSubObjectDescription.SubObjectType => StateSubObjectType.RaytracingShaderConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaytracingShaderConfig"/> struct.
        /// </summary>
        /// <param name="maxPayloadSizeInBytes">The maximum storage for scalars (counted as 4 bytes each) in ray payloads in raytracing pipelines that contain this program.</param>
        /// <param name="maxAttributeSizeInBytes">The maximum number of scalars (counted as 4 bytes each) that can be used for attributes in pipelines that contain this shader. The value cannot exceed <see cref="RaytracingMaxAttributeSizeInBytes"/>.</param>
        public RaytracingShaderConfig(int maxPayloadSizeInBytes, int maxAttributeSizeInBytes)
        {
            if (maxAttributeSizeInBytes > RaytracingMaxAttributeSizeInBytes)
            {
                throw new ArgumentOutOfRangeException($"maxAttributeSizeInBytes cannot exceed {RaytracingMaxAttributeSizeInBytes}", nameof(maxAttributeSizeInBytes));
            }

            MaxPayloadSizeInBytes = maxPayloadSizeInBytes;
            MaxAttributeSizeInBytes = maxAttributeSizeInBytes;
        }

        #region Marshal
        unsafe IntPtr IStateSubObjectDescriptionMarshal.__MarshalAlloc()
        {
            var native = Marshal.AllocHGlobal(sizeof(RaytracingShaderConfig));
            Unsafe.WriteUnaligned(native.ToPointer(), this);
            return native;
        }

        unsafe void IStateSubObjectDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
        {
            Marshal.FreeHGlobal(pDesc);
        }
        #endregion Marshal
    }
}
