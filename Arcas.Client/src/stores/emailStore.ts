// useEmailsStore.ts
import { defineStore } from 'pinia';

export const useEmailsStore = defineStore('emails', {
  state: () => ({
    nowExchangeID:'',
    emails: [] as Array<{ exchangeID: string;
                          nickname: string;
                          IDA: string; 
                          IDB: string; 
                          bookID: string; 
                          trackingID: string; 
                          isApproved: boolean; 
                          content: string; 
                          detailsA: string; 
                          detailsB: string; 
                          telephone: string }>,
  }),
  persist: true,
  actions: {
    setEmails(newEmails: Array<{ exchangeID: string; 
                                 nickname: string; 
                                 IDA: string; 
                                 IDB: string; 
                                 bookID: string; 
                                 trackingID: string; 
                                 isApproved: boolean; 
                                 content: string; 
                                 detailsA: string; 
                                 detailsB: string; 
                                 telephone: string }>) {
      this.emails = newEmails;
    },
    setNowExchangeID(exchangeID: string) {
      this.nowExchangeID = exchangeID;
    },
  },
});