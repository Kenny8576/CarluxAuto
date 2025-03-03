'use server'
import { Auction, Bid, PagedResult } from "@/types";

export async function getData(query: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${query}`);

    if(!res.ok) throw new Error("Failed to fetch data")

    return res.json();
}

export async function getBidsForAuction(id: string) : Promise<Bid[]>{
    return await fetchWrapper.get(`bids/${id}`);
}