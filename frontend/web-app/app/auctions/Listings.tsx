'use client'

import React, { useEffect, useState} from 'react'
import AuctionCard from './AuctionCard';
import AppPagination from '../components/AppPagination';
import { getData } from '../actions/auctionAction';
import Filters from './Filters';
import { useParamsStore } from '@/hooks/useParamsStore';
import qs from 'query-string';
import EmptyFilter from '../components/EmptyFilter';
import { shallow } from 'zustand/shallow';
import { useAuctionStore } from '@/hooks/useAuctionStore';


export default function Listings() {
  const [Loading, setLoading] = useState(true);

  const params = useParamsStore(state => ({
    pageNumber: state.pageNumber,
    pageSize: state.pageSize,
    searchTerm: state.searchTerm,
    orderBy: state.orderBy,
    filterBy: state.filterBy,
    seller: state.seller,
    winner: state.winner
}), shallow)

  const data = useAuctionStore(state => ({
    auctions: state.auctions,
    totalCount: state.totalCount,
    pageCount: state.pageCount
  }), shallow)

  const setData = useAuctionStore(state =>state.setData)
  const setParams = useParamsStore(state => state.setParams);
  const url = qs.stringifyUrl({ url: '', query: params});

  function setPageNumber(pageNumber: number) {
    setParams({pageNumber})
  }

    useEffect(() => {
        getData(url).then(data => {
           setData(data);
           setLoading(false)
        })
    }, [url])

    if(Loading) return <h3>Loading...</h3>

  return (
    <>
    <Filters />
    {
      data.totalCount === 0 ? (
        <EmptyFilter showReset/>
      ) : (
        <>
            <div className='grid grid-cols-4 gap-4'>
          {data.auctions.map(auction => (
              <AuctionCard auction={auction} key={auction.id}/>
          ))}
      </div>

        <div>
        <div className='flex justify-center mt-4'>
              <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber} pageCount={data.pageCount}/>
          </div>
        </div>
        </>
      )}
    </>
  )
}
