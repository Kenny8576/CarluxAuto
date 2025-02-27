/* eslint-disable react/no-unescaped-entities */
/* eslint-disable @typescript-eslint/no-explicit-any */
"use client";
import { useState } from "react";
import { useRouter } from "next/navigation";
import { fetcher } from "@/utils/AxiosClient";
import Link from "next/link";
import API from "@/utils/api";

export default function LoginPage() {

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const router = useRouter();
  const handleLogin = async (e: any) => {
    e.preventDefault();
  
    await executeAction({
      actionFn: async () => {
        try {
          const res = await fetcher(API.LOGIN, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
          });
  
          if (res.token) {
            localStorage.setItem("token", res.token);
            router.push("/");
          } else {
            alert(res.message || "Login failed");
          }
        } catch (error) {
          console.error("Login error:", error);
          alert("Something went wrong. Please try again.");
        }
      },
    });
  };

  return (
    <div className="h-screen flex items-center justify-center bg-cover bg-[url('/login-img.jpg')]">
      <div className="relative w-[578px] h-[490px] bg-white rounded-[5px] px-[0px] py-[0px] shadow-md">
        <div className="w-full p-10">
          <form onSubmit={handleLogin} >
            <h1 className="text-2xl text-[#7FDBCA]  font-bold text-pry-color text-center">CarluxMart</h1>
            <h2 className="text-2xl text-[#7FDBCA]  font-semibold text-center">Welcome back to CarluxMart</h2>

            <div className="my-2">
              <p className="text-sm font-normal">Email</p>
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="example@gmail.com"
                required
                className="w-full h-11 p-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-pry-color"
              />
            </div>

            <div className="my-2">
              <p className="text-sm font-normal">Password</p>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="*********"
                required
                className="w-full h-11 p-3 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-pry-color"
              />
            </div>

            <div className="flex justify-between text-sm">
              <Link href="/forgotPassword" className="text-pry-color underline hover:text-teal-500">
                Forgot Password?
              </Link>
            </div>

            <button
              type="submit"
              onSubmit={handleLogin}
              className="mt-3 w-full h-10 bg-[#7FDBCA] text-white rounded-[5px] cursor-pointer hover:bg-teal-500 focus:outline-none focus:ring-2 focus:ring-teal-500"
            >
              LOG IN
            </button>

            <div className="mt-3 text-sm text-center">
              <p>
                Don't have an account?{" "}
                <Link href="/signupPage" className="text-pry-color hover:text-teal-500">
                  Register
                </Link>
              </p>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
