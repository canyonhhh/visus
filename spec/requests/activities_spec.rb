require 'rails_helper'

RSpec.describe "/activities", type: :request do
  
  let(:business) { Business.create!(name: "Test Business", contact_info: "+37060000000") }

  let(:valid_attributes) do
    {
      name: "Informatics",
      description: "Teach IT",
      is_active: true,
      business_id: business.id
    }
  end

  let(:invalid_attributes) do
    {
      name: nil,
      description: "Invalid",
      is_active: "not boolean",
      business_id: nil
    }
  end

  describe "GET /index" do
    it "renders a successful response" do
      Activity.create! valid_attributes
      get activities_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      activity = Activity.create! valid_attributes
      get activity_url(activity)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_activity_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      activity = Activity.create! valid_attributes
      get edit_activity_url(activity)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new Activity" do
        expect {
          post activities_url, params: { activity: valid_attributes }
        }.to change(Activity, :count).by(1)
      end

      it "redirects to the created activity" do
        post activities_url, params: { activity: valid_attributes }
        expect(response).to redirect_to(activity_url(Activity.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new Activity" do
        expect {
          post activities_url, params: { activity: invalid_attributes }
        }.to change(Activity, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post activities_url, params: { activity: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "PATCH /update" do
    context "with valid parameters" do
      let(:new_attributes) {
        skip("Add a hash of attributes valid for your model")
      }

      it "updates the requested activity" do
        activity = Activity.create! valid_attributes
        patch activity_url(activity), params: { activity: new_attributes }
        activity.reload
        skip("Add assertions for updated state")
      end

      it "redirects to the activity" do
        activity = Activity.create! valid_attributes
        patch activity_url(activity), params: { activity: new_attributes }
        activity.reload
        expect(response).to redirect_to(activity_url(activity))
      end
    end

    context "with invalid parameters" do
      it "renders a response with 422 status (i.e. to display the 'edit' template)" do
        activity = Activity.create! valid_attributes
        patch activity_url(activity), params: { activity: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested activity" do
      activity = Activity.create! valid_attributes
      expect {
        delete activity_url(activity)
      }.to change(Activity, :count).by(-1)
    end

    it "redirects to the activities list" do
      activity = Activity.create! valid_attributes
      delete activity_url(activity)
      expect(response).to redirect_to(activities_url)
    end
  end
end
