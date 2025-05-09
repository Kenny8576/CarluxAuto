export type PagedResult<T> = {
    result: T[]
    pageCount: number
    totalCount: number
}

export type Auction = {
    reservePrice: number
    seller: string
    winner?: string
    soldAmount: number
    currentHighBid: number
    createdAt: string
    updatedAt: string
    auctionEnd: string
    status: string
    make: string
    model: string
    year: number
    color: string
    mileage: number
    imageUrl: string
    id: string
  }

  export type Identity = {
    firstname: string
    lastname: string
    email: string
    password: string
    social?: string
  }

export type LoginDetails = {
  email: string
  password: string
}

export type ResetObject = {
  email: string
  newPassword: string
  confirmPassword: string
  token: string
}

export type Bid = {
  id: string
  auctionId: string
  bidder: string
  bidTime: string
  amount: number
  bidStatus: string
}

export type AuctionFinished = {
  itemSold: boolean
  auctionId: string
  winner?: string
  seller: string
  amount: number
}
  