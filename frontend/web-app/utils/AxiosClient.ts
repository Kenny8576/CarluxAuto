import axios from "axios";

// Define Axios Instance
export const Api = axios.create({
  baseURL: "http://localhost:5000/api/Account/",
});

// Define Fetcher Function
export const fetcher = async (url: string, options: RequestInit = {}) => {
  const res = await fetch(`${Api.defaults.baseURL}${url}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });

  if (!res.ok) {
    throw new Error(`HTTP error! Status: ${res.status}`);
  }

  return res.json();
};
