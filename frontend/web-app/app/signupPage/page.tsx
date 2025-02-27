'use client'

/* eslint-disable @typescript-eslint/no-explicit-any */
import { fetcher } from '@/utils/AxiosClient';
import Link from 'next/link';
import {useRouter } from "next/navigation";
import React, { useState } from 'react'
// import GoogleSignin from '../account/googleSignin';

const socialMediaOptions = [
  'Instagram',
  'Facebook',
  'Twitter',
  'LinkedIn',
  'YouTube',
  'Pinterest',
  'Other',
];


export default function SingupPage() {

  const [firstName, setFirstname] = useState('');
  const [lastName, setLastname] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [social, setSocial] = useState('');
  const router = useRouter();

  const handleSignup = async (e: any) => {
    e.preventDefault();
    const res = await fetcher("register", {
      method: "POST",
      body: JSON.stringify({ email, password, firstName, lastName}),
    });

    console.log("Hello")
    if (res.success) {
      alert("Signup successful! Please log in.");
      router.push("/login");
    } else {
      alert(res.message || "Signup failed");
    }

  };
  return (
    <>
    <div className="min-h-screen flex items-center justify-center bg-cover bg-[url('/public/singupcarimg.jpg')] ">
    
      <div className="w-[490px] lg:w-[50%]">
        <form className="m-10 pt-5 p-10 bg-white rounded-[5px] shadow-md" onSubmit={handleSignup} >
          <h1 className="text-2xl font-bold text-[#7FDBCA] leading-[74.5px] text-center font-['Oxygen']">CarluxMart</h1>
          <h2 className="text-2xl text-[#7FDBCA]  font-semibold text-center leading-[33.5px] font-['Oxygen']">Create an account</h2>

          <div className="mb-4">
            <p className="text-sm font-normal leading-[30px]">Firstname</p>
            <input type="text" value={firstName} onChange={(e) => setFirstname(e.target.value)} placeholder="Samuel" required className="w-full h-10 p-3 border border-gray-300 rounded-sm focus:outline-none focus:ring-2 focus:ring-[#7FDBCA]" />
          </div>

          <div className="mb-4">
            <p className="text-sm font-normal leading-[30px]">Lastname</p>
            <input type="text" value={lastName} onChange={(e) => setLastname(e.target.value)} placeholder="Chukwuma" required className="w-full h-10 p-3 border border-gray-300 rounded-sm focus:outline-none focus:ring-2 focus:ring-[#7FDBCA]" />
          </div>

          <div className="mb-4">
            <p className="text-sm font-normal leading-[30px]">Email address</p>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="SamuelC@gmail.com" className="w-full h-10 p-3 border border-gray-300 rounded-sm focus:outline-none focus:ring-2 focus:ring-[#7FDBCA]" />
          </div>

          <div className="mb-4">
            <p className="text-sm font-normal leading-[30px]">Password</p>
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="*********" className="w-full h-10 p-3 border border-gray-300 rounded-sm focus:outline-none focus:ring-2 focus:ring-[#7FDBCA]" />
          </div>

          <div className="mb-4">
            <p className="text-sm font-normal leading-[30px]">How did you hear about us?</p>
            <div className="">
              <select className="w-full p-2 border border-gray-300 rounded-sm focus:outline-none focus:ring-2 focus:ring-[#7FDBCA]">
                
                {socialMediaOptions.map((option) => (
                  <option key={option} value={social} onChange={(e) => setSocial(e.target.value)}>{option}</option>
                ))}
              </select>
              <div className="absolute inset-y-0 right-0 flex items-center px-2 pointer-events-none">
                <svg className="w-4 h-4 text-gray-700" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" clipRule="evenodd" />
                </svg>
              </div>
            </div>
          </div>
          <button type="submit" onClick={handleSignup} className="mt-3 w-full h-10 bg-[#7FDBCA] text-white rounded-[5px] cursor-pointer hover:bg-teal-500 focus:outline-none focus:ring-2 focus:ring-teal-500">Signup</button>

          <div className="flex items-center justify-center my-4">
            <div className="flex-grow border-t border-gray-300"></div>
            <span className="px-2 text-gray-500 text-sm">OR</span>
            <div className="flex-grow border-t border-gray-300"></div>
          </div>
            {/* <GoogleSignin/> */}
        
          <div className="mt-3 text-sm text-center">
            <p>Already have an account? <Link href="/login" className="text-[#7FDBCA] hover:underline">Login</Link></p>
          </div>
        </form>
      </div>
    </div>
  </>
  )
}

