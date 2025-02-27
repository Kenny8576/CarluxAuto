'use client'

/* eslint-disable react/no-unescaped-entities */
/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useState } from 'react'


export default function ForgotPassword() {
const [email, setEmail] = useState('');

const submitForm = async (e: any) => {
    e.preventDefault();

    await ForgotPassword();
};

  return (
    <div
      className="text-[#7FDBCA] reset-password-page bg-cover bg-center flex justify-center items-center min-h-screen"
      
    >
      <div className="card bg-white p-12 shadow-md flex flex-col justify-center items-center w-full max-w-lg">
        <form method="post" className="w-full" onSubmit={submitForm}>
          <div className="text-center mb-6">
            <h1 className="text-pry-color text-3xl font-bold">Carluxmart</h1>
          </div>
          <div className="title-section mb-6 text-center">
            <h2 className="text-2xl font-semibold mb-4">Reset your password</h2>
            <p className="text-gray-500 text-sm">
              Enter your email below and we'll send you instructions <br />
              on how to reset your password
            </p>
          </div>
          <div className="input-group mb-6 w-full">
            <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-2">
              Email Address
            </label>
            <div className="input-with-icon relative">
              <input
                type="email" 
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="rounded-md form-input w-full py-3 pl-10 pr-4 border border-gray-300"
                placeholder="SamuelC@gmail.com"
                required
              />
              <span className="icon absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-500">&#9993;</span>
            </div>
          </div>
          <div className="input-group w-full">
            <button
              type="submit"
              className="mt-3 w-full h-10 bg-[#7FDBCA] text-white rounded-[5px] cursor-pointer hover:bg-teal-500 focus:outline-none focus:ring-2 focus:ring-teal-500"
            >
              Send reset instructions
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}
